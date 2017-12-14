using System.Collections.Generic;

namespace WinIPChanger.Desktop.Common
{
    /// <summary>
    /// Setting Info for WinIPChangerDesktop
    /// </summary>
    public class WinIPChangerSetting
    {

        /// <summary>
        /// Setting Details
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public List<WinIPChangerSettingDetail> Details { get; set; }

    }
}
