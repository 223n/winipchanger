namespace WinIPChanger.Network
{
    /// <summary>
    /// 処理結果
    /// </summary>
    public enum Results
    {

        /// <summary>
        /// 正常
        /// </summary>
        Success,

        /// <summary>
        /// 正常（要再起動）
        /// </summary>
        SuccessRebootRequired,

        /// <summary>
        /// 異常（対象のネットワーク接続が不明）
        /// </summary>
        TargetNotFoundError,

        /// <summary>
        /// 異常（インターネットプロトコルが無効）
        /// </summary>
        TargetIsNotEnableIPError,

        /// <summary>
        /// 異常（WMI APIエラー）
        /// </summary>
        ApiFailedError

    }
}
