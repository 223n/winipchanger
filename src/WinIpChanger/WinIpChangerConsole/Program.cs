using System;
using System.Globalization;
using System.Linq;
using WinIPChanger.Network;

namespace WinIpChangerConsole
{
    /// <summary>
    /// コンソールプログラムの本体
    /// </summary>
    class Program
    {

        /// <summary>
        /// メインロジックです。
        /// </summary>
        /// <param name="args">引数</param>
        static void Main(string[] args)
        {
            if (args == null || args.Length < 1)
                ShowHelp();
            else if (0 <= args[0].ToLower(CultureInfo.CurrentCulture).IndexOf("help", StringComparison.Ordinal)
                    || 0 <= args[0].ToLower(CultureInfo.CurrentCulture).IndexOf("?", StringComparison.Ordinal))
                ShowHelp();
            else if (0 <= args[0].ToLower(CultureInfo.CurrentCulture).IndexOf("s", StringComparison.Ordinal)
                    || 0 <= args[0].ToLower(CultureInfo.CurrentCulture).IndexOf("v", StringComparison.Ordinal))
                ShowAdapterList();
            else
                ShowHelp();
            Console.ReadKey();
        }

        /// <summary>
        /// ヘルプを表示します。
        /// </summary>
        static void ShowHelp()
        {
            Console.WriteLine();
            Console.WriteLine(WinIPChanger.Console.Properties.Resources.HelpMessageTitle);
            Console.WriteLine();
            Console.WriteLine(WinIPChanger.Console.Properties.Resources.HelpMessageCommand);
            Console.WriteLine();
            Console.WriteLine(WinIPChanger.Console.Properties.Resources.HelpMessageOptionS);
            Console.WriteLine(WinIPChanger.Console.Properties.Resources.HelpMessageOptionV);
            Console.WriteLine();
        }

        /// <summary>
        /// IPが有効なネットワーク接続情報を表示します。
        /// </summary>
        static void ShowAdapterList()
        {
            bool isFound = false;

            foreach(var adapter in NetworkAdapterUtility.NetworkAdaptersForIPEnabled)
            {
                if (!isFound)
                {
                    Console.WriteLine(WinIPChanger.Console.Properties.Resources.AdapterHeader);
                    Console.WriteLine(WinIPChanger.Console.Properties.Resources.AdapterHeaderSeparator);
                }
                Console.WriteLine(string.Format(
                        CultureInfo.CurrentCulture,
                        WinIPChanger.Console.Properties.Resources.AdapterListFormat,
                        adapter.Name,
                        adapter.IsDhcpEnabled,
                        adapter.IPAddress == null ? string.Empty : adapter.IPAddress.ToString(),
                        adapter.SubnetMask == null ? string.Empty : adapter.SubnetMask.ToString(),
                        adapter.DefaultGateway == null ? string.Empty : adapter.DefaultGateway.ToString(),
                        adapter.DnsServers == null || adapter.DnsServers.Count == 0 ? string.Empty : string.Join(",", adapter.DnsServers.Select(dns => dns.ToString()))
                        ));
                isFound = true;
            }
            if (isFound)
                Console.WriteLine(WinIPChanger.Console.Properties.Resources.AdapterHeaderSeparator);
            else
                Console.WriteLine(WinIPChanger.Console.Properties.Resources.AdapterNotFound);
        }

    }
}
