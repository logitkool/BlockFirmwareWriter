using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace BlockFirmwareWriter
{
    public class CommandBlock
    {
        public CommandBlock() { }

        public void WriteHex(Avrdude dude, string hexPath)
        {
            if (!canExecute(dude)) throw new InvalidOperationException("avrdudeの実行に失敗しました。");

            if (!File.Exists(hexPath))
            {
                throw new FileNotFoundException("ファームウェアファイルが存在しません。");
            }

            var args = $"-U flash:w:\"{hexPath}\":i";
            dude.Execute(args);
        }

        public void WriteRom(Avrdude dude, EEPROM rom)
        {
            if (!canExecute(dude)) throw new InvalidOperationException("avrdudeの実行に失敗しました。");

            var intel_hex_data = ":0700000001FF";
            intel_hex_data += $"{rom.GetRoleId():X2}{rom.UID_H:X2}{rom.UID_L:X2}FF{rom.GetModeId():X2}";
            var sum = 0x07 + 0x01 + 0xFF + rom.GetRoleId() + rom.UID_H + rom.UID_L + 0xFF + rom.GetModeId();
            intel_hex_data += $"{(byte)(~sum + 1):X2}";

            string tempPath = null;
            try
            {
                tempPath = Path.GetTempFileName();
                File.WriteAllText(tempPath, intel_hex_data);

                if (!dude.Execute($"-U eeprom:w:\"{tempPath}\":i").Success)
                {
                    throw new InvalidOperationException("EEPROMへの書き込みに失敗しました。接続や設定を確認してください。");
                }
            }
            finally
            {
                if (tempPath != null && File.Exists(tempPath))
                {
                    File.Delete(tempPath);
                }
            }

        }

        public EEPROM GetRom(Avrdude dude)
        {
            if (!canExecute(dude)) throw new InvalidOperationException("avrdudeの実行に失敗しました。");

            var ret = dude.Execute("-U eeprom:r:-:i -q -q");
            if ((!ret.Success) || (ret.StdOut.Split('\n').Length < 1))
            {
                throw new InvalidOperationException("EEPROMの読み込みに失敗しました。。接続や設定を確認してください。");
            }

            // :(start_code), byte count, address, record_type => 9 chars
            // flag (FF) role uidH uidL (FF) mode => 7 bytes * 2 chars = 14 chars
            // no checksum check
            var intel_hex_data = ret.StdOut.Split('\n')[0];
            var sep_hex = intel_hex_data.Skip(9).Take(14)
                .Select((v, i) => new { v, i }).GroupBy(x => x.i / 2).Select(g => g.Select(x => x.v))
                .Select(h => string.Concat(h))
                .ToArray();

            byte _saved = Convert.ToByte(sep_hex[0], 16);
            byte _role = Convert.ToByte(sep_hex[2], 16);
            byte _uid_h = Convert.ToByte(sep_hex[3], 16);
            byte _uid_l = Convert.ToByte(sep_hex[4], 16);
            byte _mode = Convert.ToByte(sep_hex[6], 16);

            var data = new EEPROM();
            data.Set(_saved, _role, _uid_h, _uid_l, _mode);

            return data;
        }

        public void WriteFuseBit(Avrdude dude, byte fuse = 0xE2)
        {
            if (!canExecute(dude)) throw new InvalidOperationException("avrdudeの実行に失敗しました。");

            var args = $"-B 3 -U lfuse:w:0x{fuse:X2}:m";
            dude.Execute(args);
        }

        public FuseBit GetFuseBit(Avrdude dude)
        {
            if (!canExecute(dude)) throw new InvalidOperationException("avrdudeの実行に失敗しました。");

            var m = new Dictionary<string, byte>();

            foreach (var name in new[] { "efuse", "hfuse", "lfuse" })
            {
                var ret = dude.Execute($"-B 3 -U {name}:r:-:d -q -q");
                if (!ret.Success || string.IsNullOrEmpty(ret.StdOut)) throw new InvalidOperationException("フューズビットの読み込みに失敗しました。");
                m[name] = Convert.ToByte(ret.StdOut.Trim(), 10);
            }

            return new FuseBit(m["efuse"], m["hfuse"], m["lfuse"]);
        }

        private bool canExecute(Avrdude avrdude)
        {
            try
            {
                return avrdude.Execute("-B 3").Success;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

    /// <summary>
    /// フューズビット
    /// </summary>
    public class FuseBit
    {
        public FuseBit(byte efuse, byte hfuse, byte lfuse)
        {
            ExtFuse = efuse;
            HFuse = hfuse;
            LFuse = lfuse;
        }

        /// <summary>
        /// Extended Fuse Byte
        /// </summary>
        public byte ExtFuse { get; }
        /// <summary>
        /// Fuse High Byte
        /// </summary>
        public byte HFuse { get; }
        /// <summary>
        /// Fuse Low Byte
        /// </summary>
        public byte LFuse { get; }

    }

    /// <summary>
    /// 
    /// </summary>
    public class EEPROM
    {
        public EEPROM() {}

        public void Set(byte saved, byte role, byte uid_h, byte uid_l, byte mode)
        {
            // Saved
            Saved = saved == 0x01;

            // Role
            if (Enum.IsDefined(typeof(FlocRole), role))
            {
                Role = (FlocRole)Enum.ToObject(typeof(FlocRole), role);
            } else
            {
                Role = null;
            }

            // Mode
            if (Enum.IsDefined(typeof(FlocMode), mode))
            {
                Mode = (FlocMode)Enum.ToObject(typeof(FlocMode), mode);
            }
            else
            {
                Mode = null;
            }

            // UID_H
            UID_H = uid_h;

            // UID_L
            UID_L = uid_l;
        }

        public byte GetRoleId()
        {
            return Role == null ? (byte)0xFF : (byte)Role;
        }

        public byte GetModeId()
        {
            return Mode == null ? (byte)0xFF : (byte)Mode;
        }

        /// <summary>
        /// 情報が保存されているか
        /// </summary>
        public bool Saved { get; private set; }

        /// <summary>
        /// UID (上位ビット)
        /// </summary>
        public byte UID_H { get; private set; }
        /// <summary>
        /// UID (下位ビット)
        /// </summary>
        public byte UID_L { get; private set; }

        /// <summary>
        /// ブロックのRole
        /// </summary>
        public FlocRole? Role { get; private set; }

        /// <summary>
        /// ブロックの実行モード
        /// </summary>
        public FlocMode? Mode { get; private set; }

    }

    /// <summary>
    /// ふろっく ブロックのRole
    /// </summary>
    public enum FlocRole : byte
    {
        MoveFront = 0x11,
        MoveBack = 0x19,

        TurnLeft = 0x21,
        TurnRight = 0x29,
        TurnLeft90 = 0x22,
        TurnRight90 = 0x2A,

        ShakeLeftHand = 0x31,
        ShakeRightHand = 0x35,
        ShakeBothHands = 0x39,

        ShakeLeftHead = 0x41,
        ShakeRightHead = 0x49,

        PureCore = 0x81,

        IfBrightness = 0x91,
        IfObject = 0x92,

        For1 = 0xC1,
        For2 = 0xC2,
        For3 = 0xC3,
        For4 = 0xC4,
        For5 = 0xC5,

        None = 0xFF
    }

    /// <summary>
    /// ふろっく コマンドブロックの動作モード
    /// </summary>
    public enum FlocMode : byte
    {
        PRODUCTION = 0x01,
        CONFIG = 0x11,
        DEBUG = 0x12
    }

}
