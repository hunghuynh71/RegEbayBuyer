using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium.Interactions;
using System.Diagnostics;
using xNet;
using RegEbayBuyer;
using System.Text.RegularExpressions;
using DeviceId;
using System.Net;

namespace CoreDogeTool
{
    public partial class fMain : Form
    {
        BackgroundWorker myBackgroundWorker = new BackgroundWorker();
        Random rand = new Random();

        volatile List<NeedInfo> g_lneedInfos = new List<NeedInfo>();



        // for sort pro5 position in window
        Queue<int> qu_position_pro5 = new Queue<int>(); 
        volatile int live_thread_number = 0;

        Queue<string> qu_cc = new Queue<string>();

        private static object locker = new object();

        public fMain()
        {
            InitializeComponent();

            myBackgroundWorker.DoWork += myBackgroundWorker_DoWork;
            myBackgroundWorker.ProgressChanged += myBackgroundWorker_ProgressChanged;
            myBackgroundWorker.RunWorkerCompleted += myBackgroundWorker_RunWorkerCompleted;

            this.dataGridViewTable.MouseDown += dataGridView_MouseDownClick;
            this.dataGridViewTable.CurrentCellDirtyStateChanged += dataGridViewTable_CurrentCellDirtyStateChanged;

            myBackgroundWorker.WorkerReportsProgress = true;
            myBackgroundWorker.WorkerSupportsCancellation = true;

            ConfigInfo.localIP = File.ReadAllText(Directory.GetCurrentDirectory() + "\\localIP.txt").Trim();
            ConfigInfo.path_911 = File.ReadAllText(Directory.GetCurrentDirectory() + "\\path_911.txt").Trim();

            this.FormClosing += this.fmain_FormClosing;
            this.dataGridViewTable.MouseClick += this.dataGridViewData_MouseClick;
        }

        private void dataGridView_MouseDownClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStripMenuControl.Show(Cursor.Position.X, Cursor.Position.Y);
            }
        }

        void dataGridViewTable_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridViewTable.IsCurrentCellDirty)
            {
                dataGridViewTable.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            dataGridViewTable.CommitEdit(DataGridViewDataErrorContexts.Commit);

        }

        void myBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            live_thread_number = 0;
            qu_position_pro5.Clear();
            for (int i = 1; i <= (int)numericUpDownThread.Value; i++)
                qu_position_pro5.Enqueue(i);

            ThreadState.allow_running = true;
            ThreadState.proxy = "";
            ThreadState.all_thread_together_running = radioButtonHMA.Checked;


            // lấy danh sách cc đã nhé
            //string[] cces = FileHelper.ReadFile(Directory.GetCurrentDirectory() + "\\cc.txt").Trim().Split('\n');
            //for (int i = 0; i < cces.Length; i++)
            //{
            //    if (!cces[i].Trim().Equals(""))
            //        qu_cc.Enqueue(cces[i].Trim());
            //}
            //if (qu_cc.Count == 0)
            //{
            //    MessageBox.Show("Không có cc??");
            //    return;
            //}     

            for (int i = 0; i < g_lneedInfos.Count; i++)
            {
                NeedInfo needInfo = new NeedInfo();

                needInfo.index = g_lneedInfos[i].index;
                needInfo.Data_input = g_lneedInfos[i].Data_input;
                needInfo.proxy = g_lneedInfos[i].proxy;
                needInfo.Result = g_lneedInfos[i].Result;
                needInfo.status = g_lneedInfos[i].status;
                needInfo.Cookie = "";
                needInfo.errorCodeStatus = g_lneedInfos[i].errorCodeStatus;

                bool tick = true;
                dataGridViewTable.Invoke( (MethodInvoker)delegate() {
                    tick = (bool)dataGridViewTable.Rows[i].Cells[1].FormattedValue;
                });
                if (!tick)
                    continue;

                if (myBackgroundWorker.CancellationPending)
                    break;

                // case 1: tat ca thread chay dong thoi, end all -> start again
                // case 2: khi mot thread hoan thanh -> thread moi duoc tao ra ngay

                // [1.1] wait for valid time
                while (!myBackgroundWorker.CancellationPending)
                {
                    // [1.1.1] live thread = 0 => reset ip
                    if (live_thread_number == 0)
                    {
                        // [option] Change ip HMA
                        if (radioButtonHMA.Checked)
                        {
                            needInfo.status = "Đang RESET IP HMA, ước tính ~30 giây...";
                            myBackgroundWorker.ReportProgress(needInfo.index, needInfo);
                            ChangeIPHelper.ChangeIPHMA();
                        }
                    }

                    // [1.1.2] if not reach max thread -> break to open new thread
                    if (live_thread_number < (int)numericUpDownThread.Value)
                        break;

                    // [1.1.3] reach max thread + all thread run together -> wait for end all -> break
                    if (live_thread_number == (int)numericUpDownThread.Value)
                    {
                        // wait for all thread finished -> open new thread with number of thread max
                        while (!myBackgroundWorker.CancellationPending && live_thread_number != 0 && ThreadState.all_thread_together_running) // if = 0 => break
                            Thread.Sleep(TimeSpan.FromSeconds(1));
                    }

                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }


                // [1.2] do work, dam bao da reset proxy o tren theo option
                if (!myBackgroundWorker.CancellationPending && ThreadState.allow_running)
                {
                    // update proxy for each thread if run together
                    if (ThreadState.all_thread_together_running)
                        needInfo.proxy = ThreadState.proxy;

                    live_thread_number++;
                    AsyncCoreDoWork(needInfo);
                }

                Thread.Sleep(3000);
            }


            // [2] wait for all thread finished
            while (live_thread_number != 0)
            {
                Thread.Sleep(1000);
            }
        }

        void myBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            NeedInfo needInfo = (NeedInfo)e.UserState;
            if (needInfo != null && needInfo.status != null)
            {
                // update for origin
                g_lneedInfos[needInfo.index].Result = needInfo.Result;
                g_lneedInfos[needInfo.index].errorCodeStatus = needInfo.errorCodeStatus;
                g_lneedInfos[needInfo.index].status = needInfo.status;

                dataGridViewTable.Rows[needInfo.index].Cells["status"].Value = needInfo.status;
                dataGridViewTable.Rows[needInfo.index].Cells["Result"].Value = needInfo.Result;

                // error
                if (needInfo.errorCodeStatus != null && needInfo.errorCodeStatus == -1)
                    dataGridViewTable.Rows[needInfo.index].Cells["status"].Style.ForeColor = Color.Red;

                // success
                if (needInfo.errorCodeStatus != null && needInfo.errorCodeStatus == 1)
                    dataGridViewTable.Rows[needInfo.index].Cells["status"].Style.ForeColor = Color.Green;

                if (needInfo.errorCodeStatus != null && needInfo.errorCodeStatus == 0)
                    dataGridViewTable.Rows[needInfo.index].Cells["status"].Style.ForeColor = Color.Black;
            }
        }

        void myBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Change the status of the buttons on the UI accordingly
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;
            buttonStart.Text = "Bắt đầu";
            MessageBox.Show("Chạy xong !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        ////////////////////////
        async Task AsyncCoreDoWork(NeedInfo needInfo)
        {
            await Task.Run(() =>
            {
                // update live thread ++ & position for chrome profile
                
                int pro5 = qu_position_pro5.Dequeue();

                // do task
                if (!myBackgroundWorker.CancellationPending)
                {
                    needInfo.status = "Bắt đầu chạy...";
                    myBackgroundWorker.ReportProgress(needInfo.index, needInfo);

                    DoTaskOne(needInfo, pro5);

                }

                // update number of thread when finished
                live_thread_number--;

                // update pro5 of position
                qu_position_pro5.Enqueue(pro5);
            });
        }


        void DoTaskOne(NeedInfo needInfo, int pro5)
        {
            while(checkBoxTmpStop.Checked)
            {
                needInfo.status = "Đang dừng, bỏ tích tạm dừng để chạy tiếp...";
                myBackgroundWorker.ReportProgress(needInfo.index, needInfo);

                Thread.Sleep(1000);
            }


            needInfo.status = "Đang chạy...";
            myBackgroundWorker.ReportProgress(needInfo.index, needInfo);

            // Lấy IP 911
            if (radioButton911.Checked)
            {
                string zipcode = (needInfo.Data_input.Split('|').Length >= 10) ? needInfo.Data_input.Split('|')[9].Trim().Split('=')[0].Trim().Split('-')[0].Trim() : "97225";
                string state = (needInfo.Data_input.Split('|').Length >= 9) ? needInfo.Data_input.Split('|')[8].Trim() : "TX";

                if (zipcode.Length != 5)
                    zipcode = "97225";

                needInfo.status = $"Đang lấy IP 911 by Zipcode {zipcode}...";
                myBackgroundWorker.ReportProgress(needInfo.index, needInfo);
                string res_by_zipcode = ChangeIPHelper.GetIPByZipCode911(zipcode, needInfo.proxy.Split(':')[1].Trim());
                if (!res_by_zipcode.Contains("Api executed successfully."))
                {
                    needInfo.status = $"Đang lấy IP 911 by State {state}...";
                    myBackgroundWorker.ReportProgress(needInfo.index, needInfo);
                    string res_by_state = ChangeIPHelper.GetIPByState911(state, needInfo.proxy.Split(':')[1].Trim());
                    if (!res_by_state.Contains("Api executed successfully."))
                    {
                        needInfo.status = "Không lấy được IP 911 !";
                        needInfo.errorCodeStatus = -1;
                        myBackgroundWorker.ReportProgress(needInfo.index, needInfo);
                        return;
                    }
                    else
                    {
                        needInfo.status = "Lấy IP 911 thành công";
                        myBackgroundWorker.ReportProgress(needInfo.index, needInfo);
                        //Thread.Sleep(4 * 1000); 
                    }

                }
                else
                {
                    needInfo.status = "Lấy IP 911 thành công";
                    myBackgroundWorker.ReportProgress(needInfo.index, needInfo);
                    //Thread.Sleep(4 * 1000);
                }
            }

            //ChromeDriver chromeDriver = null;
            try
            {
                if (needInfo.Data_input.Split('|').Length < 11)
                    throw new Exception("Không có tên thư mục ở cột 11?");

                


                //options.BinaryLocation = @"C:\Program Files\AdsPower Global\AdsPower Global.exe";

                //ChromeOptions chromeOptions = new ChromeOptions();
                ////chromeOptions.AddArgument("--no-sandbox");
                ////chromeOptions.AddArgument("--start-maximized");

                //chromeOptions.AddArgument("--disable-blink-features=AutomationControlled");
                //chromeOptions.AddExcludedArgument("enable-automation");
                //chromeOptions.AddAdditionalCapability("useAutomationExtension", false);

                ////chromeOptions.AddArgument("--disable-web-security");
                ////chromeOptions.AddArgument("--disable-blink-features=AutomationControlled");

                ////if (checkBoxDisableImage.Checked)
                ////{
                ////    //--disable-images
                ////    chromeOptions.AddArgument("--blink-settings=imagesEnabled=false");
                ////}

                //// fake useragent
                ////if (needInfo.Address != null && !needInfo.Address.Trim().Equals(""))
                ////    chromeOptions.AddArgument($"--user-agent={needInfo.Address.Trim()}");

                //// remove bar
                ////chromeOptions.AddExcludedArgument("enable-automation");
                ////chromeOptions.AddAdditionalCapability("useAutomationExtension", false);

                ////chromeOptions.AddArgument("--app=https://www.paypal.com");
                ////chrome-extension://ggmdpepbjljkkkdaklfihhngmmgmpggp/options.html

                ////chromeOptions.AddUserProfilePreference("credentials_enable_service", false);
                ////chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
                //// set position, custome here
                //#region calc position for profile
                //{
                //    // calc size
                //    int max_width = Screen.PrimaryScreen.Bounds.Width;
                //    int max_height = Screen.PrimaryScreen.Bounds.Height;
                //    int width = ConfigInfo.chrome_width;
                //    int height = ConfigInfo.chrome_height;
                //    chromeOptions.AddArgument($"--window-size={width},{height}");

                //    // calc max position for pro5
                //    int distance_x = ConfigInfo.chrome_distance_x;
                //    int distance_y = ConfigInfo.chrome_distance_y;
                //    int max_column = (max_width - width) / distance_x + 1;
                //    int max_row = (max_height - height) / distance_y + 1;

                //    // calc position (pro5 % max_column == 0) ? (pro5 / max_column) % max_row : 
                //    int row = (pro5 % max_column == 0) ? (((pro5 / max_column) % max_row == 0) ? (pro5 / max_column) % max_row + 1 : (pro5 / max_column) % max_row) : (pro5 / max_column) % max_row + 1;
                //    int column = (pro5 % max_column == 0) ? max_column : pro5 % max_column;

                //    int margin_width_postion = (column - 1) * distance_x;
                //    int margin_height_position = (row - 1) * distance_y;

                //    string position = $"--window-position={margin_width_postion},{margin_height_position}";
                //    chromeOptions.AddArgument(position);
                //}
                //#endregion


                // [option] Change ip Tinsoft
                if (radioButtonTinsoft.Checked)
                {
                    string apikey = needInfo.proxy.Trim();
                    while (!myBackgroundWorker.CancellationPending)
                    {
                        needInfo.status = "Đang lấy IP Tinsoft...";
                        needInfo.errorCodeStatus = 0;
                        myBackgroundWorker.ReportProgress(needInfo.index, needInfo);

                        string new_proxy = ChangeIPHelper.GetProxyTinsoft(apikey);
                        //Thread.Sleep(4000);
                        if (new_proxy.Contains(":") && new_proxy.Split('.').Length >= 4)
                        {
                            needInfo.status = $"Đã lấy IP mới: {new_proxy}";
                            needInfo.proxy = new_proxy;
                            myBackgroundWorker.ReportProgress(needInfo.index, needInfo);


                            break;
                        }
                        else
                        {
                            needInfo.status = $"Lỗi lấy proxy: {new_proxy}";
                            needInfo.errorCodeStatus = -1;
                            myBackgroundWorker.ReportProgress(needInfo.index, needInfo);
                            return;
                            Thread.Sleep(3000);
                        }

                        break;
                    }

                }

                if (needInfo.proxy != null &&  needInfo.proxy.Split(':').Length >= 2 && (radioButtonIPPort.Checked || radioButtonTinsoft.Checked))
                {
                    try
                    {
                        HttpRequest httpRequestProxy = new HttpRequest();
                        httpRequestProxy.UserAgent = xNet.Http.ChromeUserAgent();
                        var body = @"{
                                    " + "\n" +
                                    @"    ""user_id"": """ + needInfo.Data_input.Split('|')[10].Trim().Split('_')[0].Trim() + @""",
                                    " + "\n" +
                                    @"    ""user_proxy_config"": {
                                    " + "\n" +
                                    @"        ""proxy_soft"": ""other"",
                                    " + "\n" +
                                    @"        ""proxy_type"": ""http"",
                                    " + "\n" +
                                    @"        ""proxy_host"": """ + needInfo.proxy.Split(':')[0].Trim() + @""",
                                    " + "\n" +
                                    @"        ""proxy_port"": """ + needInfo.proxy.Split(':')[1].Trim() + @"""
                                    " + "\n" +
                                    @"    }
                                    " + "\n" +
                                    @"}";

                        if (needInfo.proxy.Split(':').Length == 4)
                        {

                            body = @"{
                                    " + "\n" +
                                    @"    ""user_id"": """ + needInfo.Data_input.Split('|')[10].Trim().Split('_')[0].Trim() + @""",
                                    " + "\n" +
                                    @"    ""user_proxy_config"": {
                                    " + "\n" +
                                    @"        ""proxy_soft"": ""other"",
                                    " + "\n" +
                                    @"        ""proxy_type"": ""http"",
                                    " + "\n" +
                                    @"        ""proxy_host"": """ + needInfo.proxy.Split(':')[0].Trim() + @""",
                                    " + "\n" +
                                    @"        ""proxy_port"": """ + needInfo.proxy.Split(':')[1].Trim() + @"""
                                    " + "\n" +
                                    @"        ""proxy_user"": """ + needInfo.proxy.Split(':')[2].Trim() + @"""
                                    " + "\n" +
                                    @"        ""proxy_password"": """ + needInfo.proxy.Split(':')[3].Trim() + @"""
                                    " + "\n" +
                                    @"    }
                                    " + "\n" +
                                    @"}";


                            //body = @"{""proxy_soft"":""other"",""proxy_type"":""http"",""proxy_host"":""" + needInfo.proxy.Split(':')[0].Trim()+ 
                            //    @""",""proxy_port"":""" + needInfo.proxy.Split(':')[1].Trim() + @""",""proxy_user"":""" + 
                            //    needInfo.proxy.Split(':')[2].Trim() + @""",""proxy_password"":""" + needInfo.proxy.Split(':')[3].Trim() + @"""}";
                        }

                        string config_url = FileHelper.ReadFile(Directory.GetCurrentDirectory() + "\\duong_dan_lay_config.txt").Trim();
                        //http://local.adspower.net:50325/api/v1/user/update
                        //http://local.adspower.net:50325/api/v1/browser/start?user_id=
                        httpRequestProxy.Post(config_url.Replace("/browser/start?user_id=", "/user/update"), body, "application/json").ToString();
                        Thread.Sleep(5000);
                    }
                    catch  {
                        throw new Exception("Set proxy http error");
                    }

                }

                if (needInfo.proxy.Split(':').Length >= 2 && (radioButtonSocks5.Checked || radioButton911.Checked))
                {
                    try
                    {
                        // set cho thăng browser
                        HttpRequest httpRequestProxy = new HttpRequest();
                        httpRequestProxy.UserAgent = xNet.Http.ChromeUserAgent();
                        var body = @"{
                                    " + "\n" +
                                    @"    ""user_id"": """ + needInfo.Data_input.Split('|')[10].Trim().Split('_')[0].Trim() + @""",
                                    " + "\n" +
                                    @"    ""user_proxy_config"": {
                                    " + "\n" +
                                    @"        ""proxy_soft"": ""other"",
                                    " + "\n" +
                                    @"        ""proxy_type"": ""socks5"",
                                    " + "\n" +
                                    @"        ""proxy_host"": """ + needInfo.proxy.Split(':')[0].Trim() + @""",
                                    " + "\n" +
                                    @"        ""proxy_port"": """ + needInfo.proxy.Split(':')[1].Trim() + @"""
                                    " + "\n" +
                                    @"    }
                                    " + "\n" +
                                    @"}";

                        if (needInfo.proxy.Split(':').Length == 4)
                        {
                            body = @"{
                                    " + "\n" +
                                    @"    ""user_id"": """ + needInfo.Data_input.Split('|')[10].Trim().Split('_')[0].Trim() + @""",
                                    " + "\n" +
                                    @"    ""user_proxy_config"": {
                                    " + "\n" +
                                    @"        ""proxy_soft"": ""other"",
                                    " + "\n" +
                                    @"        ""proxy_type"": ""socks5"",
                                    " + "\n" +
                                    @"        ""proxy_host"": """ + needInfo.proxy.Split(':')[0].Trim() + @""",
                                    " + "\n" +
                                    @"        ""proxy_port"": """ + needInfo.proxy.Split(':')[1].Trim() + @"""
                                    " + "\n" +
                                    @"        ""proxy_user"": """ + needInfo.proxy.Split(':')[2].Trim() + @"""
                                    " + "\n" +
                                    @"        ""proxy_password"": """ + needInfo.proxy.Split(':')[3].Trim() + @"""
                                    " + "\n" +
                                    @"    }
                                    " + "\n" +
                                    @"}";


                            //body = @"{""proxy_soft"":""other"",""proxy_type"":""socks5"",""proxy_host"":""" + needInfo.proxy.Split(':')[0].Trim() +
                            //    @""",""proxy_port"":""" + needInfo.proxy.Split(':')[1].Trim() + @""",""proxy_user"":""" +
                            //    needInfo.proxy.Split(':')[2].Trim() + @""",""proxy_password"":""" + needInfo.proxy.Split(':')[3].Trim() + @"""}";
                        }


                        string config_url = FileHelper.ReadFile(Directory.GetCurrentDirectory() + "\\duong_dan_lay_config.txt").Trim();
                        //http://local.adspower.net:50325/api/v1/user/update
                        //http://local.adspower.net:50325/api/v1/browser/start?user_id=
                        httpRequestProxy.Post(config_url.Replace("/browser/start?user_id=", "/user/update"), body, "application/json").ToString();
                        Thread.Sleep(5000);
                    }
                    catch
                    {
                        throw new Exception("Set proxy socks5 error");
                    }
                }


                //// set proxy & add extension
                //if (needInfo.proxy != null && !needInfo.proxy.Equals(""))
                //{
                //    if (needInfo.proxy.Split(':').Length >= 2 && !radioButtonSocks5.Checked || radioButton911.Checked)
                //        chromeOptions.AddArgument($"--proxy-server={needInfo.proxy.Trim().Split(':')[0].Trim()}:{needInfo.proxy.Trim().Split(':')[1].Trim()}");

                //    if (needInfo.proxy.Split(':').Length >= 2 && radioButtonSocks5.Checked || radioButton911.Checked)
                //        chromeOptions.AddArgument($"--proxy-server=socks5://{needInfo.proxy.Trim().Split(':')[0].Trim()}:{needInfo.proxy.Trim().Split(':')[1].Trim()}");

                //    //if (!"".Equals(needInfo.proxy.Trim()) && needInfo.proxy.Trim().Split(':').Length == 4)
                //    //    chromeOptions.AddArgument($"load-extension={Directory.GetCurrentDirectory()}\\authen_proxy_extension");
                //    //chromeOptions.AddExtension(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\extension_authen.crx");
                //}



                // thêm extension ở đây
                //string[] directories = Directory.GetDirectories($"{Directory.GetCurrentDirectory()}\\Extention");
                //if (directories.Length > 0)
                //{
                //    string all_extension = string.Join(",", directories);
                //    chromeOptions.AddArgument($"load-extension={all_extension}");

                //}

                //chromeOptions.AddArgument($"load-extension={Directory.GetCurrentDirectory()}\\Extention\\authen_proxy_extension,{Directory.GetCurrentDirectory()}\\Extention\\anti_captcha_extension");


                //if (File.Exists(Directory.GetCurrentDirectory() + "\\binary_location_chrome.txt"))
                //   chromeOptions.BinaryLocation = FileHelper.ReadFile(Directory.GetCurrentDirectory() + "\\binary_location_chrome.txt");

                // profile
                //if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\Profile"))
                //{
                //    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\Profile");
                //    Thread.Sleep(2000);
                //}

                //if (needInfo.Data_input.Split('|').Length < 11)
                //    throw new Exception("Không có tên thư mục ở cột 11?");
                //chromeOptions.AddArguments("user-data-dir=" + ConfigInfo.adsFolder + "\\" + needInfo.Data_input.Split('|')[10].Trim());

                //chrome_driver = resp["data"]["webdriver"]
                //chrome_options = Options()
                //chromeOptions.AddArguments("debuggerAddress", "127.0.0.1:52402");
                //chromeOptions.BinaryLocation = @"C:\Program Files\AdsPower Global\AdsPower Global.exe";







                //var driver = new IWebDriver(options);

                // lấy info đã nhé
                HttpRequest httpRequest = new HttpRequest();
                httpRequest.UserAgent = xNet.Http.ChromeUserAgent();
                string url = FileHelper.ReadFile(Directory.GetCurrentDirectory() + "\\duong_dan_lay_config.txt").Trim();
                string response_config = httpRequest.Get(url + needInfo.Data_input.Split('|')[10].Trim().Split('_')[0].Trim()).ToString();
                var jsonResult = (JObject)JsonConvert.DeserializeObject(response_config);
                if (!"success".Equals(jsonResult["msg"].ToString()))
                    throw new Exception("Khởi động browser lỗi?");

                var chromeDriverService = ChromeDriverService.CreateDefaultService(jsonResult["data"]["webdriver"].ToString().Replace("\\chromedriver.exe", ""));
                chromeDriverService.HideCommandPromptWindow = true;

                var options = new ChromeOptions();
                options.DebuggerAddress = jsonResult["data"]["ws"]["selenium"].ToString();

                var chromeDriver = new ChromeDriver(chromeDriverService, options);

                //C:\\Users\\Administrator\\AppData\\Roaming\\adspower_global\\cwd_global\\source\\webdriver\\chromedriver.exe

                //chromeDriver = new ChromeDriver(chromeDriverService, chromeOptions);
                chromeDriver.Manage().Timeouts().PageLoad.Add(System.TimeSpan.FromSeconds(80));

                // authen crx extension
                if (needInfo.proxy != null && !"".Equals(needInfo.proxy.Trim()) && needInfo.proxy.Trim().Split(':').Length == 4)
                {
                    try
                    {
                        var capabilities = JObject.Parse(chromeDriver.Capabilities.ToString());
                        string usrDir = capabilities["chrome"]["userDataDir"].ToString();
                        var json_Secure_Preferences = JObject.Parse(File.ReadAllText($"{usrDir}\\Default\\Secure Preferences"));

                        string extension_id = null;
                        foreach (var child in json_Secure_Preferences["extensions"]["settings"].Children())
                        {
                            try
                            {
                                string data = child.ToString();
                                string path_extension = $"{Directory.GetCurrentDirectory()}\\authen_proxy_extension";

                                string exid = data.Split('"')[1];
                                string cchild_path = child.ElementAt(0)["path"].ToString();

                                if (path_extension.Equals(cchild_path))
                                {
                                    extension_id = data.Split('"')[1];
                                    break;
                                }
                            }
                            catch { }
                        }

                        if (extension_id == null || extension_id.Trim().Equals(""))
                        {
                            extension_id = FileHelper.ReadFile(Directory.GetCurrentDirectory() + "\\extension_id.txt").Trim();
                        }

                        // string extension_id = chromeDriver.FindElementById("extension-id").Text.Replace("ID:", "").Trim();
                        chromeDriver.Url = $"chrome-extension://{extension_id}/options.html";
                        Thread.Sleep(2000);

                        chromeDriver.FindElementById("login").Clear();
                        chromeDriver.FindElementById("login").SendKeys(needInfo.proxy.Trim().Split(':')[2].Trim());
                        chromeDriver.FindElementById("password").Clear();
                        chromeDriver.FindElementById("password").SendKeys(needInfo.proxy.Trim().Split(':')[3].Trim());
                        chromeDriver.FindElementById("retry").Clear();
                        chromeDriver.FindElementById("retry").SendKeys("20");
                        chromeDriver.FindElementById("save").Click();
                        Thread.Sleep(2500);
                    }
                    catch { }
                }


                // [START TASK] update status
                needInfo.status = "Đang chạy...";
                needInfo.errorCodeStatus = 0;
                myBackgroundWorker.ReportProgress(needInfo.index, needInfo);

                chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles[0]);

                if (chromeDriver.WindowHandles.Count == 2)
                {
                    chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles[1]);
                    chromeDriver.Close();
                    chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles[0]);
                }


                try
                {
                    

                    bool bReg = false;


                    string reg_ebay = "";
                    if (checkBoxRegEbay.Checked)
                    {
                        chromeDriver.Url = "https://ebay.com";
                        Thread.Sleep(1500);

                        chromeDriver.Url = "https://ebay.com";
                        Thread.Sleep(3500);

                        if (chromeDriver.WindowHandles.Count == 2)
                        {
                            chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles[1]);
                            chromeDriver.Close();
                            chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles[0]);
                        }

                        chromeDriver.Manage().Cookies.DeleteAllCookies();
                        Thread.Sleep(500);
                        chromeDriver.Manage().Cookies.DeleteAllCookies();
                        Thread.Sleep(500);

                        // click vào nút reg
                        try
                        {
                            if (chromeDriver.FindElementsByXPath("//a[contains(@href,'https://signin.ebay.com/ws/eBayISAPI.dll?SignIn&ru=https')]").Count > 0)
                            {
                                chromeDriver.FindElementsByXPath("//a[contains(@href,'https://signin.ebay.com/ws/eBayISAPI.dll?SignIn&ru=https')]")[0].Click();
                                Thread.Sleep(3000);
                            }

                            //*[@id='create-account-link']
                            if (chromeDriver.FindElementsByXPath("//*[@id='create-account-link']").Count > 0)
                            {
                                chromeDriver.FindElementsByXPath("//*[@id='create-account-link']")[0].Click();
                                Thread.Sleep(3000);
                            }
                        }
                        catch 
                        { 
                            //try
                            //{
                            //    chromeDriver.ExecuteScript("");
                            //    Thread.Sleep(3000);
                            //}
                            //catch { }
                        
                        }


                        if (!chromeDriver.Url.Contains("https://signup.ebay.com/pa/crte"))
                        {
                            chromeDriver.Url = "https://signup.ebay.com/pa/crte?ru=https%3A%2F%2Fwww.ebay.com%2F";
                            Thread.Sleep(3500);
                        }


                        

                        needInfo.status = "Đang tạo tài khoản...";
                        needInfo.errorCodeStatus = 0;
                        myBackgroundWorker.ReportProgress(needInfo.index, needInfo);

                        // nếu dính captcha thì bypass captcha trước
                        if ((chromeDriver.Url.Contains("captcha?") || chromeDriver.Url.Contains("crte?"))  && chromeDriver.FindElementsById("Email").Count == 0)
                        {
                            chromeDriver.Url = chromeDriver.Url;
                            Thread.Sleep(3500);

                            if (!chromeDriver.PageSource.Contains("sitekey="))
                                throw new Exception("Không tìm thấy siteKey");

                            string site_key = chromeDriver.PageSource.Split(new[] { "sitekey=" }, StringSplitOptions.None)[1].Split('&')[0];

                            for (int i = 0; i < 5; i++)
                            {
                                needInfo.status = $"Đang giải captcha {i + 1}/{5}...";
                                needInfo.errorCodeStatus = 0;
                                myBackgroundWorker.ReportProgress(needInfo.index, needInfo);

                                string response = SolveCaptcha(ConfigInfo.apiKeyCaptcha, site_key);
                                if (response.Equals(""))
                                    throw new Exception("Không giải được captcha");

                                chromeDriver.ExecuteScript($"document.getElementById('anycaptchaSolveButton').onclick('{response}');");
                                Thread.Sleep(5000);

                                if (!chromeDriver.Url.Contains("captcha?"))
                                    break;
                            }

                            if ((!chromeDriver.Url.Contains("captcha?") || chromeDriver.Url.Contains("crte?")) && chromeDriver.FindElementsById("Email").Count == 0)
                            {
                                needInfo.status = "Giải captcha ok, đang tạo tk...";
                                needInfo.errorCodeStatus = 0;
                                myBackgroundWorker.ReportProgress(needInfo.index, needInfo);
                            }   
                            
                        }


                        if (chromeDriver.Url.Contains("captcha?") && chromeDriver.FindElementsById("Email").Count == 0)
                        {
                            throw new Exception("Không giải được captcha");
                        }


                        string fullName = needInfo.Data_input.Split('|')[3].Trim();
                        string firstName = fullName.Split(' ')[0].Trim();
                        string lastName = fullName.Split(' ')[1].Trim();
                        
                        // Fill Mail
                        //string email = needInfo.Email;
                        //string email = needInfo.DATA_INPUT.Split('|')[14].Trim();
                        string email = needInfo.Data_input.Split('|')[0].Trim();
                        needInfo.Email = email;
                        chromeDriver.FindElementById("Email").Clear();
                        Thread.Sleep(500);
                        chromeDriver.FindElementById("Email").SendKeys(email);
                        Thread.Sleep(1500);

                        // password
                        needInfo.Password = needInfo.Data_input.Split('|')[2].Trim();

                        chromeDriver.FindElementById("password").Clear();
                        Thread.Sleep(500);
                        chromeDriver.FindElementById("password").SendKeys(needInfo.Password);
                        Thread.Sleep(1500);
                        chromeDriver.FindElementById("password").Clear();
                        Thread.Sleep(500);
                        chromeDriver.FindElementById("password").SendKeys(needInfo.Password);


                        // Reg Account
                        Thread.Sleep(2500);

                        chromeDriver.FindElementById("firstname").SendKeys(firstName);
                        Thread.Sleep(1500);
                        chromeDriver.FindElementById("lastname").SendKeys(lastName);
                        Thread.Sleep(1500);


                        chromeDriver.ExecuteScript("document.getElementById('EMAIL_REG_FORM_SUBMIT').click()");
                        //chromeDriver.FindElementById("EMAIL_REG_FORM_SUBMIT").Click();
                        Thread.Sleep(10500);

                        // check trường hợp code từ mail
                        if (chromeDriver.FindElements(By.XPath("//button[@name='SEND_RECLAIM_EMAIL']")).Count > 0)
                        {
                            FileHelper.WriteAppendToFile(Directory.GetCurrentDirectory() + "\\output\\log_error_da_ton_tai.txt", $"{needInfo.index + 1} | {needInfo.Data_input}");
                            throw new Exception("Tài khoản đã tồn tại !!!");
                        }

                        if (!chromeDriver.Url.StartsWith("https://reg.ebay.com/") && !chromeDriver.Url.Equals("https://www.ebay.com/"))
                        {
                            FileHelper.WriteAppendToFile(Directory.GetCurrentDirectory() + "\\output\\log_error_da_ton_tai.txt", $"{needInfo.index + 1} | {needInfo.Data_input}");
                            throw new Exception("Tài khoản lỗi, đã tồn tại??? !!!");
                        }

                        needInfo.status = "Tạo tài khoản thành công, đang setup...";
                        needInfo.errorCodeStatus = 0;
                        myBackgroundWorker.ReportProgress(needInfo.index, needInfo);

                        reg_ebay = "->Reg_success";
                        bReg = true;
                    }




                    // lấy user
                    if (!checkBoxGoogle.Checked)
                    {
                        try
                        {

                            needInfo.status = $"Lấy địa chỉ...";
                            needInfo.errorCodeStatus = 0;
                            myBackgroundWorker.ReportProgress(needInfo.index, needInfo);

                            try
                            {
                                chromeDriver.Url = "https://www.ebay.com/";
                                Thread.Sleep(3000);
                                if (!chromeDriver.PageSource.Contains("sitekey="))
                                    throw new Exception("Không tìm thấy siteKey");

                                string site_key = chromeDriver.PageSource.Split(new[] { "sitekey=" }, StringSplitOptions.None)[1].Split('&')[0];

                                for (int i = 0; i < 5; i++)
                                {
                                    needInfo.status = $"Đang giải captcha {i + 1}/{5}...";
                                    needInfo.errorCodeStatus = 0;
                                    myBackgroundWorker.ReportProgress(needInfo.index, needInfo);

                                    string response = SolveCaptcha(ConfigInfo.apiKeyCaptcha, site_key);
                                    if (response.Equals(""))
                                        throw new Exception("Không giải được captcha");

                                    chromeDriver.ExecuteScript($"document.getElementById('anycaptchaSolveButton').onclick('{response}');");
                                    Thread.Sleep(5000);

                                    if (!chromeDriver.Url.Contains("captcha?"))
                                        break;
                                }
                            }
                            catch { }

                            try
                            {
                                if (chromeDriver.FindElementsById("pass").Count > 0)
                                {
                                    chromeDriver.FindElementsById("pass")[0].SendKeys(needInfo.Data_input.Split('|')[2].Trim());
                                    Thread.Sleep(3000);
                                    chromeDriver.FindElementsById("pass")[0].SendKeys(OpenQA.Selenium.Keys.Enter);
                                    Thread.Sleep(3000);

                                }
                            }
                            catch { }

                            //try
                            //{
                            //    chromeDriver.FindElementById("gh-ug").Click();
                            //    Thread.Sleep(3000);
                            //}
                            //catch (Exception ex) { }

                            try
                            {
                                if (needInfo.Result.Trim().Equals(""))
                                    needInfo.Result += chromeDriver.PageSource.Split(new[] { "&userid=" }, StringSplitOptions.None)[1].Split('>')[0].Split('"')[0];
                            }
                            catch { }

                            try
                            {
                                if (needInfo.Result.Trim().Equals(""))
                                    needInfo.Result += chromeDriver.PageSource.Split(new[] { "userIdSignIn\":\"" }, StringSplitOptions.None)[1].Split('>')[0].Split('"')[0];
                            }
                            catch { }

                            try
                            {

                                if (needInfo.Result.Trim().Equals(""))
                                {
                                    needInfo.Result += chromeDriver.PageSource.Split(new[] { "https://www.ebay.com/usr/" }, StringSplitOptions.None)[1].Split('>')[0].Split('"')[0];
                                }
                            }
                            catch { }

                        }
                        catch { }
                    }

                    if (checkBoxGetCookie.Checked)
                    {
                        try
                        {
                            if (!Directory.Exists($"{ Directory.GetCurrentDirectory()}\\cookie"))
                            {
                                Directory.CreateDirectory($"{ Directory.GetCurrentDirectory()}\\cookie");
                            }

                            // lấy cookie
                            // get cookie
                            needInfo.status = "Đang lấy cookie...";
                            needInfo.errorCodeStatus = 0;
                            myBackgroundWorker.ReportProgress(needInfo.index, needInfo);

                            chromeDriver.Url = "https://www.ebay.com/";
                            Thread.Sleep(3000);
                            


                            // login ebay
                            try
                            {
                                if (chromeDriver.FindElementsByXPath("//*[@id='gh-ug']//a[contains(@href,'https://signin.ebay.com')]").Count > 0)
                                {
                                    chromeDriver.FindElementsByXPath("//*[@id='gh-ug']//a[contains(@href,'https://signin.ebay.com')]")[0].Click();
                                    Thread.Sleep(3000);

                                    chromeDriver.FindElementsByXPath("//*[@id='userid']")[0].SendKeys(needInfo.Data_input.Split('|')[0].Trim());
                                    Thread.Sleep(1000);

                                    chromeDriver.FindElementsByXPath("//*[@id='userid']")[0].SendKeys(OpenQA.Selenium.Keys.Enter);
                                    Thread.Sleep(2000);

                                    chromeDriver.FindElementsByXPath("//*[@id='pass']")[0].SendKeys(needInfo.Data_input.Split('|')[2].Trim());
                                    Thread.Sleep(1000);

                                    chromeDriver.FindElementsByXPath("//*[@id='pass']")[0].SendKeys(OpenQA.Selenium.Keys.Enter);
                                    Thread.Sleep(5000);
                                }
                            }
                            catch { }

                            try
                            {
                                if (chromeDriver.FindElementsById("pass").Count > 0)
                                {
                                    chromeDriver.FindElementsById("pass")[0].SendKeys(needInfo.Data_input.Split('|')[2].Trim());
                                    Thread.Sleep(3000);
                                    chromeDriver.FindElementsById("pass")[0].SendKeys(OpenQA.Selenium.Keys.Enter);
                                    Thread.Sleep(3000);

                                }

                            }
                            catch
                            {

                            }



                            chromeDriver.Url = "https://www.ebay.com/";
                            Thread.Sleep(3000);

                            List<dCookie> dCookies = new List<dCookie>();
                            //MessageBox.Show(chromeDriver.Manage().Cookies.AllCookies.Count.ToString());
                            for (int i = 0; i < chromeDriver.Manage().Cookies.AllCookies.Count; i++)
                            {
                                try
                                {
                                    dCookie dcookie = new dCookie();
                                    dcookie.domain = ".ebay.com";

                                    if (chromeDriver.Manage().Cookies.AllCookies[i].Expiry == null)
                                    {
                                        dcookie.expirationDate = new DateTime(2030, 2, 2).Subtract(new DateTime(1970, 1, 1)).TotalSeconds; ;
                                    }
                                    else
                                    {
                                        DateTime _dt = (DateTime)chromeDriver.Manage().Cookies.AllCookies[i].Expiry;
                                        dcookie.expirationDate = _dt.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                                    }

                                    //DateTime dt = (DateTime)chromeDriver.Manage().Cookies.AllCookies[i].Expiry;
                                    //dcookie.expirationDate = dt.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

                                    dcookie.hostOnly = false;
                                    dcookie.httpOnly = chromeDriver.Manage().Cookies.AllCookies[i].IsHttpOnly; // oke
                                    dcookie.name = chromeDriver.Manage().Cookies.AllCookies[i].Name;
                                    dcookie.path = chromeDriver.Manage().Cookies.AllCookies[i].Path;
                                    dcookie.sameSite = "unspecified";
                                    dcookie.secure = chromeDriver.Manage().Cookies.AllCookies[i].Secure; // oke

                                    dcookie.session = false; // oke
                                    if (chromeDriver.Manage().Cookies.AllCookies[i].Name.ToString().Equals("ds1") ||
                                        chromeDriver.Manage().Cookies.AllCookies[i].Name.ToString().Equals("ebay") ||
                                        chromeDriver.Manage().Cookies.AllCookies[i].Name.ToString().Equals("s"))
                                        dcookie.session = true;


                                    dcookie.storeId = "0";
                                    dcookie.value = chromeDriver.Manage().Cookies.AllCookies[i].Value;
                                    dCookies.Add(dcookie);
                                }
                                catch { }
                            }

                            dCookieJ2TeamFormat _dCookieJ2TeamFormat = new dCookieJ2TeamFormat();
                            _dCookieJ2TeamFormat.url = "https://www.ebay.com";
                            _dCookieJ2TeamFormat.cookies = dCookies;
                            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(_dCookieJ2TeamFormat);
                            needInfo.Cookie = jsonString;
                            FileHelper.WriteToFile($"{Directory.GetCurrentDirectory()}\\cookie\\{needInfo.Data_input.Split('@')[0].Replace("@", "_").Trim()}_{DateTime.Now.Hour}_{DateTime.Now.Minute}_{DateTime.Now.Second}.json", jsonString);



                        }
                        catch (Exception ex) {
                            //MessageBox.Show(ex.Message);
                        }
                    }

                    string add_dia_chi = "";
                    if (checkBoxAddAddress.Checked)
                    {
                        // add địa chỉ và phone
                        try
                        {
                            chromeDriver.Url = "https://accountsettings.ebay.com/uas";
                            Thread.Sleep(5000);

                            try
                            {
                                if (!chromeDriver.PageSource.Contains("sitekey="))
                                    throw new Exception("Không tìm thấy siteKey");

                                string site_key = chromeDriver.PageSource.Split(new[] { "sitekey=" }, StringSplitOptions.None)[1].Split('&')[0];

                                for (int i = 0; i < 5; i++)
                                {
                                    needInfo.status = $"Đang giải captcha {i + 1}/{5}...";
                                    needInfo.errorCodeStatus = 0;
                                    myBackgroundWorker.ReportProgress(needInfo.index, needInfo);

                                    string response = SolveCaptcha(ConfigInfo.apiKeyCaptcha, site_key);
                                    if (response.Equals(""))
                                        throw new Exception("Không giải được captcha");

                                    chromeDriver.ExecuteScript($"document.getElementById('anycaptchaSolveButton').onclick('{response}');");
                                    Thread.Sleep(5000);

                                    if (!chromeDriver.Url.Contains("captcha?"))
                                        break;
                                }
                            }
                            catch { }

                            if (chromeDriver.FindElementsById("pass").Count > 0)
                            {
                                chromeDriver.FindElementsById("pass")[0].SendKeys(needInfo.Data_input.Split('|')[2].Trim());
                                Thread.Sleep(3000);
                                chromeDriver.FindElementsById("pass")[0].SendKeys(OpenQA.Selenium.Keys.Enter);
                                Thread.Sleep(3000);

                            }

                            string address = needInfo.Data_input.Split('|')[5 + 1].Trim();
                            string city = needInfo.Data_input.Split('|')[6 + 1].Trim();
                            string state = needInfo.Data_input.Split('|')[7 + 1].Trim();
                            string zipcode = needInfo.Data_input.Split('|')[8 + 1].Trim().Split('-')[0];
                            string phone = needInfo.Data_input.Split('|')[4 + 1].Trim();

                            if (chromeDriver.FindElementsByXPath("//*[@id='countryId']//option[@selected='selected']").Count > 0 && chromeDriver.FindElementByXPath("//*[@id='countryId']//option[@selected='selected']").Text.Equals("Vietnam"))
                            {
                                chromeDriver.FindElementsByXPath("//input[@name='address1']")[0].SendKeys(address);
                                chromeDriver.FindElementsByXPath("//input[@name='city']")[0].SendKeys(city);
                                chromeDriver.FindElementsByXPath("//input[@name='state']")[0].SendKeys(state);
                                chromeDriver.FindElementsByXPath("//input[@name='zip']")[0].SendKeys(zipcode);
                                chromeDriver.FindElementsByXPath("//input[@name='phoneFlagComp1']")[0].SendKeys(phone);
                                Thread.Sleep(2000);

                                chromeDriver.FindElementsByXPath("//input[@name='phoneFlagComp1']")[0].SendKeys(OpenQA.Selenium.Keys.Enter);
                                Thread.Sleep(5000);


                                if (chromeDriver.FindElementsByXPath("//input[@name='address1']").Count > 0)
                                    throw new Exception("Error adress");

                            }
                            else if (chromeDriver.FindElementsByXPath("//*[@id='countryId']//option[@selected='selected']").Count == 0 || chromeDriver.FindElementByXPath("//*[@id='countryId']//option[@selected='selected']").Text.Equals("United States"))
                            {
                                string add = $"{address}|{city}|{state}|{zipcode}";
                                chromeDriver.FindElementById("addressSugg").SendKeys(add);
                                Thread.Sleep(6500);

                                try
                                {
                                    if (chromeDriver.FindElements(By.Id("addressSugg_listitem0")).Count >= 2)
                                    {
                                        throw new Exception("Sai địa chỉ, nhiều option");
                                    }    

                                    if (chromeDriver.FindElements(By.Id("addressSugg_listitem0")).Count > 0)
                                        chromeDriver.FindElements(By.Id("addressSugg_listitem0"))[0].Click();
                                    Thread.Sleep(3500);
                                }
                                catch 
                                {
                                    chromeDriver.ExecuteScript("document.getElementById('addressSugg_listitem0').click()");
                                    Thread.Sleep(3500);
                                }

                                chromeDriver.FindElementsByXPath("//input[@name='phoneFlagComp1']")[0].SendKeys(phone);
                                Thread.Sleep(2000);

                                chromeDriver.FindElementsByXPath("//input[@name='phoneFlagComp1']")[0].SendKeys(OpenQA.Selenium.Keys.Enter);
                                Thread.Sleep(5000);


                                if (chromeDriver.FindElementsByXPath("//input[@name='address1']").Count > 0)
                                    throw new Exception("Error adress");
                            }
                        


                            add_dia_chi = $"->Add_dia_chi: Success";
                        }
                        catch (Exception ex)
                        {
                            add_dia_chi = $"->Add_dia_chi: {ex.Message}";
                        }
                    }

                    string very_mail = "";
                    if (checkBoxVerymail.Checked)
                    {
                        needInfo.status = "Đang very mail...";
                        needInfo.errorCodeStatus = 0;
                        myBackgroundWorker.ReportProgress(needInfo.index, needInfo);

                        //...
                        try
                        {
                            chromeDriver.Url = "https://accountsettings.ebay.com/profile";
                            Thread.Sleep(3000);

                            try
                            {
                                if (!chromeDriver.PageSource.Contains("sitekey="))
                                    throw new Exception("Không tìm thấy siteKey");

                                string site_key = chromeDriver.PageSource.Split(new[] { "sitekey=" }, StringSplitOptions.None)[1].Split('&')[0];

                                for (int i = 0; i < 5; i++)
                                {
                                    needInfo.status = $"Đang giải captcha {i + 1}/{5}...";
                                    needInfo.errorCodeStatus = 0;
                                    myBackgroundWorker.ReportProgress(needInfo.index, needInfo);

                                    string response = SolveCaptcha(ConfigInfo.apiKeyCaptcha, site_key);
                                    if (response.Equals(""))
                                        throw new Exception("Không giải được captcha");

                                    chromeDriver.ExecuteScript($"document.getElementById('anycaptchaSolveButton').onclick('{response}');");
                                    Thread.Sleep(5000);

                                    if (!chromeDriver.Url.Contains("captcha?"))
                                        break;
                                }
                            }
                            catch { }

                            if (chromeDriver.FindElementsById("pass").Count > 0)
                            {
                                chromeDriver.FindElementsById("pass")[0].SendKeys(needInfo.Data_input.Split('|')[2].Trim());
                                Thread.Sleep(3000);
                                chromeDriver.FindElementsById("pass")[0].SendKeys(OpenQA.Selenium.Keys.Enter);
                                Thread.Sleep(3000);

                            }

                            //chromeDriver.FindElementsById("pass")[0].SendKeys(needInfo.Data_input.Split('|')[2].Trim());
                            //Thread.Sleep(3000);
                            //chromeDriver.FindElementsById("pass")[0].SendKeys(OpenQA.Selenium.Keys.Enter);
                            //Thread.Sleep(3000);

                            if (chromeDriver.FindElementsById("individual_email_address_verify_button").Count == 0)
                            {
                                throw new Exception("Error button Verify, Đã Verified??");
                            }
                            chromeDriver.ExecuteScript("document.getElementById('individual_email_address_verify_button').click()");
                            Thread.Sleep(3000);

                            string code = "";
                            try
                            {
                                chromeDriver.ExecuteScript("window.open('https://hotmail.com/', '_blank');");
                                Thread.Sleep(2500);
                                chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last());
                                Thread.Sleep(3500);

                                needInfo.mail = needInfo.Data_input.Split('|')[0].Trim();
                                needInfo.pass_mail = needInfo.Data_input.Split('|')[1].Trim();

                                int iResLogin = ImapHelper.Login(chromeDriver, needInfo, false);
                                if (iResLogin == Constant.LG_INVALID_MAIL)
                                    throw new Exception("Đăng nhập lỗi: Mail không hợp lệ");
                                else if (iResLogin == Constant.LG_WRONG_PASSWORD)
                                    throw new Exception("Đăng nhập lỗi: Sai mật khẩu");
                                else if (iResLogin == Constant.LG_ERROR_CODE_2)
                                    throw new Exception("Đăng nhập lỗi: Không nhận được code lần 2");
                                else if (iResLogin == Constant.LG_ERROR_CODE_1)
                                    throw new Exception("Đăng nhập lỗi: Không nhận được code lần 1");
                                else if (iResLogin == Constant.LG_ERROR_UNDEFINED)
                                    throw new Exception("Đăng nhập lỗi: [undefined]");
                                else if (iResLogin == Constant.LG_RQ_ADD_PHONE)
                                {
                                }
                                else if (iResLogin == Constant.LG_RQ_ADD_MAIL)
                                {
                                    //throw new Exception("Đăng nhập lỗi: Yêu cầu add mail khôi phục");
                                }
                                else if (iResLogin == Constant.LG_RQ_VERY_PHONE)
                                    throw new Exception("Đăng nhập lỗi: Yêu cầu very code phone");
                                else if (iResLogin == Constant.LG_RQ_VERY_MAIL)
                                    throw new Exception("Đăng nhập lỗi: Yêu cầu verymail");
                                else if (iResLogin == Constant.LG_RQ_RCV_MAIL)
                                    throw new Exception("Đăng nhập lỗi: Thiếu mail khôi phục");

                                // readl all
                                try
                                {
                                    chromeDriver.Url = ("https://outlook.live.com/mail");
                                    Thread.Sleep(6000);

                                    // gotit
                                    if (chromeDriver.FindElements(By.XPath($"//div[contains(@class,'ms-Callout-main')]//button")).Count > 0)
                                    {
                                        try
                                        {
                                            chromeDriver.FindElements(By.XPath($"//div[contains(@class,'ms-Callout-main')]//button"))[0].Click();
                                            Thread.Sleep(2000);
                                        }
                                        catch { }
                                    }

                                    //chromeDriver.FindElements(By.XPath($"//div[@aria-label='Select all messages']"))[0].Click();
                                    //Thread.Sleep(2000);

                                    //if (chromeDriver.FindElements(By.XPath($"//button[contains(@aria-label,'Empty Focused')]")).Count > 0)
                                    //{
                                    //    chromeDriver.FindElements(By.XPath($"//button[contains(@aria-label,'Empty Focused')]"))[0].Click();
                                    //    Thread.Sleep(3500);
                                    //}
                                    //else if (chromeDriver.FindElements(By.XPath($"//button[contains(@aria-label,'Empty folder')]")).Count > 0)
                                    //{
                                    //    chromeDriver.FindElements(By.XPath($"//button[contains(@aria-label,'Empty folder')]"))[0].Click();
                                    //    Thread.Sleep(3500);
                                    //}


                                    //chromeDriver.FindElements(By.XPath($"//button[contains(@id,'ok-1')]"))[0].Click();
                                    //Thread.Sleep(3500);
                                }
                                catch { }


                                for (int retry = 0; retry < 5; retry++)
                                {
                                    if (code.Length >= 6)
                                        break;

                                    chromeDriver.Url = "https://outlook.live.com/mail/0/inbox";
                                    Thread.Sleep(3000);

                                    // gotit
                                    if (chromeDriver.FindElements(By.XPath($"//div[contains(@class,'ms-Callout-main')]//button")).Count > 0)
                                    {
                                        try
                                        {
                                            chromeDriver.FindElements(By.XPath($"//div[contains(@class,'ms-Callout-main')]//button"))[0].Click();
                                            Thread.Sleep(2000);
                                        }
                                        catch { }
                                    }

                                    if (chromeDriver.FindElementsByXPath("//button[contains(@aria-label,'Close')]").Count > 0)
                                    {
                                        try
                                        {
                                            chromeDriver.FindElementsByXPath("//button[contains(@aria-label,'Close')]")[0].Click();
                                            Thread.Sleep(3000);
                                        }
                                        catch { }
                                    }
                                    // check close Gotit
                                    // gotit
                                    if (chromeDriver.FindElements(By.XPath($"//div[contains(@class,'ms-Callout-main')]//button")).Count > 0)
                                    {
                                        try
                                        {
                                            chromeDriver.FindElements(By.XPath($"//div[contains(@class,'ms-Callout-main')]//button"))[0].Click();
                                            Thread.Sleep(2000);
                                        }
                                        catch { }
                                    }

                                    for (int i = 0; i < chromeDriver.FindElementsByXPath("//div[contains(@aria-label,'eBay security code')]").Count; i++)
                                    {
                                        try
                                        {
                                            string aria = chromeDriver.FindElementsByXPath("//div[contains(@aria-label,'eBay security code')]")[i].GetAttribute("aria-label");
                                            code = Regex.Match(aria, ": (\\d+) ").Groups[1].Value.Trim();
                                            if (code.Length >= 6)
                                                break;
                                        }
                                        catch { }
                                    }

                                    if (code.Length >= 6)
                                        break;

                                }
                            }
                            catch {
                            }

                            chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles[0]);

                            if (code.Equals(""))
                                throw new Exception("Không lấy được code");

                            chromeDriver.FindElementsByXPath("//input[@name='securityCode']")[0].SendKeys(code + OpenQA.Selenium.Keys.Enter);
                            Thread.Sleep(5000);
                            if (chromeDriver.FindElementsByXPath("//input[@name='securityCode']").Count != 0)
                                throw new Exception("Không xác thực được code");

                            very_mail = "->very_mail: Success";
                        }
                        catch (Exception ex)
                        {
                            very_mail = $"->very_mail: {ex.Message}";
                        }
                    }


                    string tuong_tac = "";
                    if (checkBoxTuongTac.Checked)
                    {
                        needInfo.status = "Đang tương tác...";
                        needInfo.errorCodeStatus = 0;
                        myBackgroundWorker.ReportProgress(needInfo.index, needInfo);

                        try
                        {
                            chromeDriver.Url = "https://ebay.com";
                            Thread.Sleep(3000);
                            if (!chromeDriver.PageSource.Contains("sitekey="))
                                throw new Exception("Không tìm thấy siteKey");

                            string site_key = chromeDriver.PageSource.Split(new[] { "sitekey=" }, StringSplitOptions.None)[1].Split('&')[0];

                            for (int i = 0; i < 5; i++)
                            {
                                needInfo.status = $"Đang giải captcha {i + 1}/{5}...";
                                needInfo.errorCodeStatus = 0;
                                myBackgroundWorker.ReportProgress(needInfo.index, needInfo);

                                string response = SolveCaptcha(ConfigInfo.apiKeyCaptcha, site_key);
                                if (response.Equals(""))
                                    throw new Exception("Không giải được captcha");

                                chromeDriver.ExecuteScript($"document.getElementById('anycaptchaSolveButton').onclick('{response}');");
                                Thread.Sleep(5000);

                                if (!chromeDriver.Url.Contains("captcha?"))
                                    break;
                            }
                        }
                        catch { }

                        try
                        {
                            if (chromeDriver.FindElementsById("pass").Count > 0)
                            {
                                chromeDriver.FindElementsById("pass")[0].SendKeys(needInfo.Data_input.Split('|')[2].Trim());
                                Thread.Sleep(3000);
                                chromeDriver.FindElementsById("pass")[0].SendKeys(OpenQA.Selenium.Keys.Enter);
                                Thread.Sleep(3000);

                            }
                        }
                        catch { }

                        //...
                        try
                        {
                            for (int i = 0; i < (int) numericUpDownMaxTuongTac.Value; i++)
                            {
                                needInfo.status = $"Đang tương tác lần {i + 1}/{numericUpDownMaxTuongTac.Value}...";
                                needInfo.errorCodeStatus = 0;
                                myBackgroundWorker.ReportProgress(needInfo.index, needInfo);

                                string[] keyword = FileHelper.ReadFile(Directory.GetCurrentDirectory() + "\\keyword.txt").Trim().Split('\n');
                                OpenSearchEbayChrome(keyword[rand.Next(0, keyword.Length - 1)].Trim(), chromeDriver);
                            }


                            very_mail = "->tuong_tac: Success";
                        }
                        catch (Exception ex)
                        {
                            very_mail = $"->tuong_tac: {ex.Message}";
                        }
                    }


                  

                    needInfo.status = $"Done{reg_ebay}{add_dia_chi}{very_mail}{tuong_tac}";
                    needInfo.errorCodeStatus = 1;
                    myBackgroundWorker.ReportProgress(needInfo.index, needInfo);

                    if (!Directory.Exists($"{Directory.GetCurrentDirectory()}\\output\\{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}"))
                    {
                        Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}\\output\\{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}");
                    }

                    if (checkBoxRegEbay.Checked)
                        FileHelper.WriteAppendToFile(Directory.GetCurrentDirectory() + $"\\output\\{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}\\reg_success.txt", $"{needInfo.Result}| {needInfo.Data_input} | {needInfo.status}");
                    else
                    {
                        FileHelper.WriteAppendToFile(Directory.GetCurrentDirectory() + $"\\output\\{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}\\update_success.txt", $"{needInfo.Result}| {needInfo.Data_input} | {needInfo.status}");

                    }

                    if (bReg)
                    {
                        needInfo.status = $"Chờ ngâm [0]/[{numericUpDownMaxTuongTac.Value}]->Done{reg_ebay}{add_dia_chi}{very_mail}{tuong_tac}";
                        needInfo.errorCodeStatus = 1;
                        myBackgroundWorker.ReportProgress(needInfo.index, needInfo);

                        for (int i = 0; i < (int)numericUpDownMaxTuongTac.Value; i++)
                        {
                            needInfo.status = $"Chờ ngâm [{i + 1}]/[{numericUpDownMaxTuongTac.Value}]->Done{reg_ebay}{add_dia_chi}{very_mail}{tuong_tac}";
                            needInfo.errorCodeStatus = 1;
                            myBackgroundWorker.ReportProgress(needInfo.index, needInfo);

                            Thread.Sleep(1000);
                        }

                        needInfo.status = $"Done{reg_ebay}{add_dia_chi}{very_mail}{tuong_tac}";
                        needInfo.errorCodeStatus = 1;
                        myBackgroundWorker.ReportProgress(needInfo.index, needInfo);
                    }



                }
                catch (Exception ex)
                {
                    needInfo.status = ex.Message;
                    needInfo.errorCodeStatus = -1;
                    myBackgroundWorker.ReportProgress(needInfo.index, needInfo);

                    if (!Directory.Exists($"{Directory.GetCurrentDirectory()}\\output\\{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}"))
                    {
                        Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}\\output\\{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}");
                    }


                    FileHelper.WriteAppendToFile(Directory.GetCurrentDirectory() + $"\\output\\{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}\\log_error.txt", $"{needInfo.index + 1} | {needInfo.Data_input} | {needInfo.proxy}| {needInfo.Result}|{needInfo.status}");

                }


            }
            catch (Exception ex)
            {
                needInfo.status = ex.Message;
                needInfo.errorCodeStatus = -1;
                myBackgroundWorker.ReportProgress(needInfo.index, needInfo);

                if (!Directory.Exists($"{Directory.GetCurrentDirectory()}\\output\\{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}"))
                {
                    Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}\\output\\{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}");
                }
                FileHelper.WriteAppendToFile(Directory.GetCurrentDirectory() + $"\\output\\{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}\\log_error.txt", $"{needInfo.index + 1}|{needInfo.Data_input} | {needInfo.proxy}| {needInfo.Result}|{needInfo.status}");

            }
            finally
            {
                if (checkBoxTurnOff.Checked)
                {

                    //http://local.adspower.net:50325/api/v1/browser/stop?user_id=
                    try
                    {
                        HttpRequest httpRequest = new HttpRequest();
                        httpRequest.UserAgent = xNet.Http.ChromeUserAgent();
                        string url = FileHelper.ReadFile(Directory.GetCurrentDirectory() + "\\duong_dan_lay_config.txt").Trim();
                        httpRequest.Get(url.Replace("/start?user_id=", "/stop?user_id=") + needInfo.Data_input.Split('|')[10].Trim().Split('_')[0].Trim());
                    }
                    catch { }

                    //try
                    //{
                    //    chromeDriver.Close();
                    //    chromeDriver.Quit();
                    //}
                    //catch (Exception ex) 
                    //{
                    //    //FileHelper.WriteAppendToFile(Directory.GetCurrentDirectory() + "\\log_chromedriver.txt", ex.Message);
                    //}

                }

                FileHelper.WriteAppendToFile(Directory.GetCurrentDirectory() + "\\output\\log_activity.txt", $"{needInfo.index + 1} | {needInfo.Data_input} | {needInfo.proxy}| {needInfo.Result}|{needInfo.status}");
            }
        }


        bool OpenSearchEbayChrome(string keyword, ChromeDriver chromeDriver)
        {
            try
            {
                if (keyword.Equals(""))
                    keyword = "watch";

                chromeDriver.Url = "http://ebay.com";
                IJavaScriptExecutor js = (IJavaScriptExecutor)chromeDriver;
                chromeDriver.FindElementByXPath("//input[@placeholder='Search for anything']").SendKeys(keyword.Trim() + OpenQA.Selenium.Keys.Enter);
                Thread.Sleep(2000);
                //chromeDriver.FindElementByXPath("//input[@value='Search']").Click();
                //Thread.Sleep(5000);

                // SCroll
                try
                {
                    for (int i = 0; i <= 30; i++)
                    {
                        js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight * " + i + " / 100)");
                        i += 2;
                        Thread.Sleep(1000 * new Random().Next(0, 3));
                    }


                    js.ExecuteScript("window.scrollTo(0, 0)");


                    // xem sản phẩm ngẫu nhiên
                    try
                    {
                        int link_random = rand.Next(0, chromeDriver.FindElementsByXPath("//a[@class='s-item__link']").Count);
                        string link =  chromeDriver.FindElementsByXPath("//a[@class='s-item__link']")[link_random].GetAttribute("href");
                        chromeDriver.Url = link;

                        for (int i = 0; i <= 30; i++)
                        {
                            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight * " + i + " / 100)");
                            i += 2;
                            Thread.Sleep(1000 * new Random().Next(0, 3));
                        }
                    }
                    catch { }

                    // xem ảnh
                    try
                    {
                        js.ExecuteScript("window.scrollTo(0, 0)");
                        chromeDriver.FindElements(By.XPath("//table[@role='presentation']//img[@itemprop='image']"))[0].Click();
                        Thread.Sleep(6500);
                    }
                    catch { }

                    Thread.Sleep(1000 * new Random().Next(2, 5));
                }
                catch { }


                return true;
            }
            catch { }


            return false;
        }


        string SolveCaptcha(string apiKey, string websiteKey)
        {
            try
            {
                string taskId = createTask(apiKey, websiteKey);
                if (taskId.Equals(""))
                    return "";

                Thread.Sleep(3000);
                for (int i = 0; i < 50; i++)
                {
                    Thread.Sleep(1000);

                    string response = getResult(apiKey, taskId);
                    if (response.Equals(""))
                        continue;

                    if (response.Equals("ERROR"))
                        break;

                    if (!response.Equals(""))
                        return response;

 
                }
            }
            catch { }

            return "";
        }


        string createTask(string apiKey, string websiteKey)
        {
            try
            {
                HttpRequest httpRequest = new HttpRequest();
                var body = @"{
                        " + "\n" +
                        @"	""clientKey"": """ + apiKey + @""",
                        " + "\n" +
                        @"	""task"": {
                        " + "\n" +
                        @"		""type"": ""HCaptchaTaskProxyless"",
                        " + "\n" +
                        @"		""websiteURL"": ""https://www.ebay.com/"",
                        " + "\n" +
                        @"		""websiteKey"": """ + websiteKey + @"""
                        " + "\n" +
                        @"	}
                        " + "\n" +
                        @"}";
                string response = httpRequest.Post("https://api.anycaptcha.com/createTask", body, "application/json").ToString();
                JObject jObject = JObject.Parse(response);

                return jObject["taskId"].ToString();
            }
            catch { }

            return "";
        }


        string getResult(string apiKey, string taskId)
        {
            try
            {
                HttpRequest httpRequest = new HttpRequest();
                var body = @"{
                            " + "\n" +
                            @"	""clientKey"": """ + apiKey + @""",
                            " + "\n" +
                            @"	""taskId"": " + taskId + @"
                            " + "\n" +
                            @"	
                            " + "\n" +
                            @"}";
                string response = httpRequest.Post("https://api.anycaptcha.com/getTaskResult", body, "application/json").ToString();
                JObject jObject = JObject.Parse(response);

                if (!jObject["errorCode"].ToString().Equals("SUCCESS"))
                    return "ERROR";

                if (jObject["status"].ToString().Equals("processing"))
                    return "";

                if (jObject["status"].ToString().Equals("ready"))
                    return jObject["solution"]["gRecaptchaResponse"].ToString();
            }
            catch { }

            return "";
        }

        ////////////////////////////////////////////////////////////////////////
        private void buttonStart_Click(object sender, EventArgs e)
        {
            ConfigInfo.adsFolder = textBoxAdsFolder.Text.Trim();

            try
            {
                string data_init = FileHelper.ReadFile(Directory.GetCurrentDirectory() + "\\init_info.json");
                JObject jObject = JObject.Parse(data_init);
                ConfigInfo.chrome_width = Int32.Parse(jObject["chrome_width"].ToString());
                ConfigInfo.chrome_height = Int32.Parse(jObject["chrome_height"].ToString());
                ConfigInfo.chrome_distance_x = Int32.Parse(jObject["chrome_distance_x"].ToString());
                ConfigInfo.chrome_distance_y = Int32.Parse(jObject["chrome_distance_y"].ToString());
            }
            catch { }

            ConfigInfo.apiKeyCaptcha = textBoxApiCaptcha.Text.Trim();
            ConfigInfo.nhamang = textBoxNhaMang.Text.Trim();


            buttonStart.Enabled = false;
            buttonStop.Enabled = true;
            myBackgroundWorker.RunWorkerAsync();
        }

        private void buttonImportFile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.FilterIndex = 0;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string[] data = File.ReadAllText(openFileDialog1.FileName).Trim().Split('\n');

                    g_lneedInfos.Clear();

                    int order_number = 0;
                    for (int i = 0; i < data.Length; i++)
                    {
                        if (data[i].Trim().StartsWith("ID") || data[i].Trim().StartsWith("FirstName"))
                            continue;

                        NeedInfo needInfo = new NeedInfo();
                        needInfo.index = order_number;      

                        needInfo.Data_input = data[i].Trim();
                        needInfo.status = "";
                        needInfo.Result = "";
                        needInfo.errorCodeStatus = 0;

                        g_lneedInfos.Add(needInfo);

                        order_number++;
                    }

                    // Update to table
                    dataGridViewTable.Rows.Clear();
                    for (int i = 0; i < g_lneedInfos.Count; i++)
                    {
                        int index = dataGridViewTable.Rows.Add();
                        dataGridViewTable.Invoke((MethodInvoker)delegate ()
                        {
                            dataGridViewTable.Rows[index].Cells["index"].Value = g_lneedInfos[i].index + 1;
                            dataGridViewTable.Rows[index].Cells["DATA_INPUT"].Value = g_lneedInfos[i].Data_input;
                        });
                    }

                    MessageBox.Show($"Nhập thành công {g_lneedInfos.Count} dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        

        private void linkLabelCopyKey_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetText(toolStripStatusLabelKey.Text);
            MessageBox.Show("Đã copy key thành công, CTRL + V vào notepad để lấy key !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        ////////////////////////////////////////////////////////////////////////
        void InitValue()
        {
            try
            {

                string key = new DeviceIdBuilder().AddMachineName().AddProcessorId().AddMotherboardSerialNumber()
                                                .AddSystemDriveSerialNumber()
                                                .ToString();
                using (var sha = new System.Security.Cryptography.SHA256Managed())
                {
                    byte[] textData = System.Text.Encoding.UTF8.GetBytes("REB" + key);
                    byte[] hash = sha.ComputeHash(textData);
                    toolStripStatusLabelKey.Text = "REB" + (BitConverter.ToString(hash).Replace("-", String.Empty)).ToString().Substring(0, 19);
                    
                    
                }
            }
            catch { }
        }

        void InitInfoApp()
        {


            if (CheckKeyLicense() )
            {
                buttonImportFile.Enabled = true;
                buttonImportProxy.Enabled = true;
                buttonStart.Enabled = true;
            }
            else
            {
                buttonImportFile.Enabled = false;
                buttonImportProxy.Enabled = false;
                buttonStart.Enabled = false;
                buttonStop.Enabled = false;
            }


        }

        bool CheckKeyLicense()
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpRequest httpRequest = new HttpRequest();
                httpRequest.ReadWriteTimeout = 15000;
                httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.131 Safari/537.36";// Http.ChromeUserAgent();
                httpRequest.IgnoreProtocolErrors = true;
                httpRequest.SslCertificateValidatorCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                string response = httpRequest.Get("https://github.com/phamsytruong/minh_hieu_reg_buyer/blob/main/key", null).ToString();
                if (response.Contains(toolStripStatusLabelKey.Text))
                {
                    return true;
                }
            }
            catch { }
            
            return false;

        }


        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (myBackgroundWorker.IsBusy)
            {
                buttonStart.Text = "Đang dừng...";
                myBackgroundWorker.CancelAsync();
                myBackgroundWorker.Dispose();
            }

            // turn of all chromedriver
            ProcessHelper.KillAllProcessTree("chromedriver");
        }

        private void fmain_FormClosing(Object sender, FormClosingEventArgs e)
        {
            DialogResult d = MessageBox.Show("Bạn có chắc chắn muốn thoát ứng dụng?", "QA", MessageBoxButtons.YesNo);
            if (d == DialogResult.No)
                e.Cancel = true;
            else
            {
                ProcessHelper.KillAllProcessTree("chromedriver");

            }
        }

        private void toolStripStatusLabelCopyKey_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(toolStripStatusLabelKey.Text);
            MessageBox.Show("Copy key thành công, CTRL + V vào notepad để lấy key !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            InitValue();
            InitInfoApp();
        }

        private void MenuItemNew_Click(Object sender, System.EventArgs e)
        {
            //List<string> ldata = new List<string>();
            //for (int i = 0; i < g_lneedInfos.Count; i++)
            //{
            //    NeedInfo needInfo = g_lneedInfos[i];
            //    string row = $"{needInfo.index + 1} | {needInfo.Email} | {needInfo.Pass} |{needInfo.User} | {needInfo.Name} | {needInfo.Address} | {needInfo.City}  | {needInfo.State} | {needInfo.SState} | {needInfo.PostCode} | {needInfo.Proxy} | {needInfo.Result} |{needInfo.status}";
            //    ldata.Add(row);
            //}

            //FileHelper.WriteToFile($"{Directory.GetCurrentDirectory()}\\export_data.txt", string.Join("\n", ldata));
            //MessageBox.Show("Đã xuất thông tin ra file export_data.txt !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridViewData_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                int currentMouseOverRow = dataGridViewTable.HitTest(e.X, e.Y).RowIndex;

                if (currentMouseOverRow >= 0)
                {
                    if (g_lneedInfos.Count > 0)
                        m.MenuItems.Add(new MenuItem(string.Format("Xuất file data?", currentMouseOverRow.ToString()), MenuItemNew_Click));
                }

                m.Show(dataGridViewTable, new Point(e.X, e.Y));
            }
        }

        private void buttonImportProxy_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.FilterIndex = 0;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string[] data = File.ReadAllText(openFileDialog1.FileName).Trim().Split('\n');
                    if (data.Length == 1 && data[0].Trim().Equals(""))
                    {
                        throw new Exception("File proxy rỗng");
                    }


                    // Update to table
                    for (int i = 0; i < g_lneedInfos.Count; i++)
                    {
                        g_lneedInfos[i].proxy = data[i % data.Length].Trim();
                        dataGridViewTable.Invoke((MethodInvoker)delegate ()
                        {
                            dataGridViewTable.Rows[i].Cells["proxy"].Value = g_lneedInfos[i].proxy;
                        });
                    }

                    MessageBox.Show($"Cập nhật thành công proxy vào {g_lneedInfos.Count} dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void radioButton911_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < g_lneedInfos.Count; i++)
            {
                g_lneedInfos[i].proxy = $"{ConfigInfo.localIP.Trim()}:{(5000 + i % 100).ToString()}";
                dataGridViewTable.Invoke((MethodInvoker)delegate ()
                {
                    dataGridViewTable.Rows[i].Cells["proxy"].Value = g_lneedInfos[i].proxy;
                });
            }
        }

        private void radioButtonNone_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < g_lneedInfos.Count; i++)
            {
                g_lneedInfos[i].proxy = "";
                dataGridViewTable.Invoke((MethodInvoker)delegate ()
                {
                    //dataGridViewTable.Rows[i].Cells["uid"].Value = needInfos[i].uid;
                    //dataGridViewTable.Rows[i].Cells["password"].Value = needInfos[i].password;
                    //dataGridViewTable.Rows[i].Cells["_2fa"].Value = needInfos[i]._2fa;
                    //dataGridViewTable.Rows[i].Cells["cookie"].Value = needInfos[i].cookie;
                    //dataGridViewTable.Rows[i].Cells["userAgent"].Value = g_lneedInfos[i].Address;
                    dataGridViewTable.Rows[i].Cells["proxy"].Value = g_lneedInfos[i].proxy;
                    //dataGridViewTable.Rows[i].Cells["status"].Value = "";
                });
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (!File.Exists($"{Directory.GetCurrentDirectory()}\\cc.txt"))
                {
                    FileHelper.WriteAppendToFile($"{Directory.GetCurrentDirectory()}\\cc.txt", "");
                }
                Process.Start("explorer.exe", $"{Directory.GetCurrentDirectory()}\\cc.txt");
            }
            catch { }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
            
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (!Directory.Exists($"{Directory.GetCurrentDirectory()}\\output"))
                {
                    Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}\\output");
                }

                Process.Start("explorer.exe", $"{Directory.GetCurrentDirectory()}\\output");
            }
            catch { }
        }

        private void selectALLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int r = 0; r < dataGridViewTable.Rows.Count; r++)
            {
                dataGridViewTable.Rows[r].Cells["Tick"].Value = true;
            }
        }

        private void unSelectALLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int r = 0; r < dataGridViewTable.Rows.Count; r++)
            {
                dataGridViewTable.Rows[r].Cells["Tick"].Value = false;
            }
        }

        private void selectByDragToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int r = 0; r < dataGridViewTable.Rows.Count; r++)
            {
                if (dataGridViewTable.Rows[r].Cells[2].Selected)
                {
                    dataGridViewTable.Rows[r].Cells[1].Value = true;
                }
                else
                {
                    if ((bool)dataGridViewTable.Rows[r].Cells[1].FormattedValue)
                    {
                        dataGridViewTable.Rows[r].Cells[1].Value = false;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Form1().ShowDialog();
        }
    }
}
