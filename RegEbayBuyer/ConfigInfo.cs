using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreDogeTool
{
    class ConfigInfo
    {
        // Cau hinh thong tin lien quan ve giao dien chrome tai day
        public static int chrome_width = 500;
        public static int chrome_height = 600;
        public static int chrome_distance_x = 400;
        public static int chrome_distance_y = 300;


        internal static string nhamang;
        internal static string apiKeyCaptcha;
        internal static string adsFolder;


        // variable of data
        public static string path_911 { get; set; }
        public static string localIP { get; set; }
        public static string link_order { get; set; }


        public static string[] keywords { get; set; }
        public static string simpleApiAccessToken { get; set; }
        
    }

    class ThreadState
    {
        public static bool all_thread_together_running { get; set; }
        public static string proxy { get; set; }
        public static bool allow_running { get; set; }
    }
}
