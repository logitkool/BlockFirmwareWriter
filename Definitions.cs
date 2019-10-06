namespace BlockFirmwareWriter
{
    /// <summary>
    /// 実行時情報
    /// </summary>
    class ExecInfo
    {
        public ExecInfo(bool success, string stdout = null)
        {
            Success = success;
            StdOut = stdout;
        }

        public bool Success { get; }
        public string StdOut { get; }
    }

    /// <summary>
    /// フューズビット
    /// </summary>
    class FuseBit
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
    /// ふろっく ブロックのRole
    /// </summary>
    enum FlocRole : byte
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
    enum FlocMode : byte
    {
        PRODUCTION = 0x01,
        CONFIG = 0x11,
        DEBUG = 0x12
    }

}
