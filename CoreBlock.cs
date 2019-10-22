using System;
using System.IO;

namespace BlockFirmwareWriter
{
    public class CoreBlock
    {
        public CoreBlock() { }

        public void WriteBin(EspTool esptool, string comPort, string binPath)
        {
            if (!File.Exists(binPath))
            {
                throw new ArgumentException(".binファイルのパスを設定してください。");
            }

            var args = "--before default_reset --after hard_reset ";
            args += "write_flash -z --flash_mode dio --flash_freq 80m --flash_size detect ";
            args += $"0x10000 {binPath}";
            esptool.Execute(comPort, args);
        }

    }
}
