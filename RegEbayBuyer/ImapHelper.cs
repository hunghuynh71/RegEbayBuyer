using CoreDogeTool;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RegEbayBuyer
{
    class ImapHelper
    {
        public static int Login(ChromeDriver chromeDriver, NeedInfo needInfo, bool socks5)
        {
            int iResLogin = LoginExecutor(chromeDriver, needInfo, socks5);


            if (chromeDriver.Url.Contains("https://outlook.live.com/mail"))
                return Constant.LG_SUCCESS;

            if (chromeDriver.FindElements(By.Id("iShowSkip")).Count > 0)
            {
                try
                {
                    chromeDriver.FindElements(By.Id("iShowSkip"))[0].Click();
                    Thread.Sleep(4500);
                }
                catch { }
            }


            if (chromeDriver.FindElementsById("idSIButton9").Count > 0)
            {
                try
                {
                    chromeDriver.FindElementsById("idSIButton9")[0].Click();
                    Thread.Sleep(4500);

                    if (chromeDriver.Url.Contains("https://outlook.live.com/mail"))
                        return Constant.LG_SUCCESS;
                }
                catch { }
            }
            return iResLogin;
        }
        public static int LoginExecutor(ChromeDriver chromeDriver, NeedInfo needInfo, bool socks5)
        {
            try
            {
                string userName = needInfo.mail.Trim();
                string password = needInfo.pass_mail.Trim();

                chromeDriver.Navigate().GoToUrl("https://outlook.live.com/owa/?nlp=1");
                Thread.Sleep(2000);

                if (!ChromeDriverHelper.QuickAction(chromeDriver, "Id", "i0116", 0, userName + OpenQA.Selenium.Keys.Enter, 5, "SENDKEY"))
                    return Constant.LG_ERROR_UNDEFINED;
                if (!ChromeDriverHelper.QuickAction(chromeDriver, "Id", "i0118", 0, password + OpenQA.Selenium.Keys.Enter, 5, "SENDKEY"))
                    return Constant.LG_ERROR_UNDEFINED;

                Thread.Sleep(4500);
                if (chromeDriver.PageSource.Contains("Your account or password is incorrect"))
                    return Constant.LG_WRONG_PASSWORD;

                // check nếu hiện hoạt động ko bình thường
                if (chromeDriver.PageSource.Contains("We have detected unusual activity on your Microsoft"))
                {
                    if (chromeDriver.FindElementsById("iLandingViewAction").Count > 0)
                    {
                        chromeDriver.FindElementsById("iLandingViewAction")[0].Click();
                        Thread.Sleep(3000);

                        if (chromeDriver.FindElements(By.Id("iPhoneSection")).Count > 0 && (chromeDriver.FindElements(By.Id("iPhoneSection"))[0].GetAttribute("style").Contains("block") || chromeDriver.FindElements(By.Id("iPhoneSection"))[0].GetAttribute("style").Equals("")))
                            return Constant.LG_RQ_ADD_PHONE;

                        if (chromeDriver.FindElements(By.Id("iEmailSection")).Count > 0 && chromeDriver.FindElements(By.Id("iEmailSection"))[0].GetAttribute("style").Contains("block"))
                            return Constant.LG_RQ_ADD_MAIL;
                    }
                }


                // check nếu xác minh mail
                if (chromeDriver.FindElementsById("iProofEmail").Count > 0)
                {
                    return Constant.LG_RQ_RCV_MAIL;

                    //if (needInfo.mail.Split(':').Length < 3)
                    //    return Constant.LG_RQ_RCV_MAIL;

                    //string currenRcvMail = needInfo.mail.Split(':')[2].Trim();
                    //// khởi tạo cookie trước để sau nhận code
                    //if (needInfo.cookie_rcv == null || needInfo.cookie_rcv.Equals(""))
                    //    needInfo.cookie_rcv = InitCookieForMail(currenRcvMail, needInfo.proxy, socks5);
                    //Thread.Sleep(2500);

                    //if (chromeDriver.FindElementsByXPath("//input[contains(@value,'@')]").Count > 0)
                    //{
                    //    chromeDriver.FindElementsByXPath("//input[contains(@value,'@')]")[0].Click();
                    //    Thread.Sleep(2000);
                    //}

                    //if (!ChromeDriverHelper.QuickAction(chromeDriver, "Id", "iProofEmail", 0, currenRcvMail.Split(':')[0].Trim(), 10, "SENDKEY"))
                    //    return Constant.LG_ERROR_UNDEFINED;
                    //if (!ChromeDriverHelper.QuickAction(chromeDriver, "Id", "iSelectProofAction", 0, null, 10, "CLICK"))
                    //    return Constant.LG_ERROR_UNDEFINED;


                    //// sau đó nhân code ở đây
                    //Thread.Sleep(3000); ;
                    //string code = GetCodeRcvMail(needInfo.cookie_rcv, currenRcvMail, needInfo.proxy, socks5);

                    //if (code == null)
                    //    return Constant.LG_ERROR_CODE_1;
                    //if (!ChromeDriverHelper.QuickAction(chromeDriver, "Id", "iOttText", 0, code + Keys.Enter, 10, "SENDKEY"))
                    //    return Constant.LG_ERROR_UNDEFINED;

                    //Thread.Sleep(5000);
                }

                // check success
                if (chromeDriver.Url.Contains("https://outlook.live.com/mail"))
                    return Constant.LG_SUCCESS;

                if (chromeDriver.FindElements(By.Id("iShowSkip")).Count > 0)
                {
                    try
                    {
                        chromeDriver.FindElements(By.Id("iShowSkip"))[0].Click();
                        Thread.Sleep(4500);
                    }
                    catch { }
                }

                if (chromeDriver.FindElements(By.Id("iCancel")).Count > 0)
                {
                    try
                    {
                        chromeDriver.FindElements(By.Id("iCancel"))[0].Click();
                        Thread.Sleep(4500);
                    }
                    catch { }
                }

                try
                {
                    chromeDriver.FindElements(By.XPath("//input[@type='submit']"))[0].Click();
                    Thread.Sleep(5000);

                    if (chromeDriver.Url.Contains("https://outlook.live.com/mail"))
                        return Constant.LG_SUCCESS;

                    if (chromeDriver.FindElements(By.Id("iPhoneSection")).Count > 0 && (chromeDriver.FindElements(By.Id("iPhoneSection"))[0].GetAttribute("style").Contains("block") || chromeDriver.FindElements(By.Id("iPhoneSection"))[0].GetAttribute("style").Equals("")))
                        return Constant.LG_RQ_ADD_PHONE;

                    if (chromeDriver.FindElements(By.Id("iEmailSection")).Count > 0 && (chromeDriver.FindElements(By.Id("iEmailSection"))[0].GetAttribute("style").Contains("block") || chromeDriver.FindElements(By.Id("iEmailSection"))[0].GetAttribute("style").Equals("")))
                        return Constant.LG_RQ_ADD_MAIL;
                }
                catch { }


                if (chromeDriver.FindElements(By.Id("iCancel")).Count > 0)
                {
                    try
                    {
                        chromeDriver.FindElements(By.Id("iCancel"))[0].Click();
                        Thread.Sleep(5000);
                    }
                    catch { }
                }

                if (chromeDriver.Url.Contains("https://outlook.live.com/mail"))
                    return Constant.LG_SUCCESS;

                // add mail to phone
                if (chromeDriver.PageSource.Contains("Help us protect your account"))
                {
                    if (chromeDriver.FindElements(By.Id("iPhoneSection")).Count > 0 && (chromeDriver.FindElements(By.Id("iPhoneSection"))[0].GetAttribute("style").Contains("block") || chromeDriver.FindElements(By.Id("iPhoneSection"))[0].GetAttribute("style").Equals("")))
                        return Constant.LG_RQ_ADD_PHONE;

                    if (chromeDriver.FindElements(By.Id("iEmailSection")).Count > 0 && chromeDriver.FindElements(By.Id("iEmailSection"))[0].GetAttribute("style").Contains("block"))
                        return Constant.LG_RQ_ADD_MAIL;
                }


                // check if locked????
                // Show locked => Start
                if (chromeDriver.FindElements(By.Id("StartAction")).Count > 0)
                {
                    chromeDriver.FindElements(By.Id("StartAction"))[0].Click();
                    Thread.Sleep(3500);

                    if (chromeDriver.FindElements(By.Id("iPhoneSection")).Count > 0 && (chromeDriver.FindElements(By.Id("iPhoneSection"))[0].GetAttribute("style").Contains("block") || chromeDriver.FindElements(By.Id("iPhoneSection"))[0].GetAttribute("style").Equals("")))
                        return Constant.LG_RQ_ADD_PHONE;

                    if (chromeDriver.FindElements(By.Id("iEmailSection")).Count > 0 && chromeDriver.FindElements(By.Id("iEmailSection"))[0].GetAttribute("style").Contains("block"))
                        return Constant.LG_RQ_ADD_MAIL;
                }

                // Show Verify
                if (chromeDriver.FindElements(By.Id("iLandingViewAction")).Count > 0)
                {
                    chromeDriver.FindElements(By.Id("iLandingViewAction"))[0].Click();
                    Thread.Sleep(2500);

                    if (chromeDriver.FindElements(By.Id("iProofList")).Count > 0 &&
                        chromeDriver.FindElements(By.Id("iProofList"))[0].Text.Contains("@"))
                        return Constant.LG_RQ_VERY_MAIL;

                    if (chromeDriver.FindElements(By.Id("iProofList")).Count > 0 &&
                        chromeDriver.FindElements(By.XPath("//input[contains(@value,'SMS')]")).Count > 0)
                        return Constant.LG_RQ_VERY_PHONE;

                    if (chromeDriver.FindElements(By.Id("iPhoneSection")).Count > 0 && (chromeDriver.FindElements(By.Id("iPhoneSection"))[0].GetAttribute("style").Contains("block") || chromeDriver.FindElements(By.Id("iPhoneSection"))[0].GetAttribute("style").Equals("")))
                        return Constant.LG_RQ_ADD_PHONE;

                    if (chromeDriver.FindElements(By.Id("iEmailSection")).Count > 0 && chromeDriver.FindElements(By.Id("iEmailSection"))[0].GetAttribute("style").Contains("block"))
                        return Constant.LG_RQ_ADD_MAIL;
                }

                if (chromeDriver.FindElements(By.Id("idSIButton9")).Count > 0)
                {
                    chromeDriver.FindElements(By.Id("idSIButton9"))[0].Click();
                    Thread.Sleep(2500);

                    if (chromeDriver.FindElements(By.Id("iProofList")).Count > 0 &&
                        chromeDriver.FindElements(By.Id("iProofList"))[0].Text.Contains("@"))
                        return Constant.LG_RQ_VERY_MAIL;

                    if (chromeDriver.FindElements(By.Id("iProofList")).Count > 0 &&
                        chromeDriver.FindElements(By.XPath("//input[contains(@value,'SMS')]")).Count > 0)
                        return Constant.LG_RQ_VERY_PHONE;

                    if (chromeDriver.FindElements(By.Id("iPhoneSection")).Count > 0 && (chromeDriver.FindElements(By.Id("iPhoneSection"))[0].GetAttribute("style").Contains("block") || chromeDriver.FindElements(By.Id("iPhoneSection"))[0].GetAttribute("style").Equals("")))
                        return Constant.LG_RQ_ADD_PHONE;

                    if (chromeDriver.FindElements(By.Id("iEmailSection")).Count > 0 && chromeDriver.FindElements(By.Id("iEmailSection"))[0].GetAttribute("style").Contains("block"))
                        return Constant.LG_RQ_ADD_MAIL;

                    if (chromeDriver.Url.Contains("https://outlook.live.com/mail"))
                        return Constant.LG_SUCCESS;
                }

            }
            catch { }

            return Constant.LG_ERROR_UNDEFINED;
        }


    }

    public class Constant
    {
        // Login hotmail
        public const int LG_INVALID_MAIL = -4;
        public const int LG_WRONG_PASSWORD = -3;
        public const int LG_ERROR_CODE_2 = -2;
        public const int LG_ERROR_CODE_1 = -1;
        public const int LG_ERROR_UNDEFINED = 0;
        public const int LG_SUCCESS = 1;
        public const int LG_RQ_ADD_PHONE = 2;
        public const int LG_RQ_VERY_PHONE = 3;
        public const int LG_RQ_VERY_MAIL = 4;
        public const int LG_RQ_RCV_MAIL = 5;
        public const int LG_RQ_ADD_MAIL = 6;

        // Add phone hotmail
        public const int ADD_PHONE_ERROR_GET_PHONE = -3;
        public const int ADD_PHONE_ERROR_CODE_2 = -2;
        public const int ADD_PHONE_ERROR_CODE_1 = -1;
        public const int ADD_PHONE_ERROR_UNDEFINED = 0;
        public const int ADD_PHONE_SUCCESS = 1;


        // ADD RCV hotmaiL
        public const int ADD_RCV_ERROR_INVALID_MAIL = -4;
        public const int ADD_RCV_ERROR_RQ_RCV = -3;
        public const int ADD_RCV_ERROR_CODE_2 = -2;
        public const int ADD_RCV_ERROR_CODE_1 = -1;
        public const int ADD_RCV_ERROR_UNDEFINED = 0;
        public const int ADD_RCV_SUCCESS = 1;

        // CHANGE PASS hotmaiL
        public const int CHANGE_PASS_ERROR_RQ_ADD_RCV = -5;
        public const int CHANGE_PASS_ERROR_RQ_RCV = -4;
        public const int CHANGE_PASS_ERROR_RQ_PHONE = -3;
        public const int CHANGE_PASS_ERROR_CODE_2 = -2;
        public const int CHANGE_PASS_ERROR_CODE_1 = -1;
        public const int CHANGE_PASS_ERROR_UNDEFINED = 0;
        public const int CHANGE_PASS_SUCCESS = 1;

        // ADD_FORWARD hotmaiL
        public const int ADD_FORWARD_ERROR_RQ_ADD_RCV = -5;
        public const int ADD_FORWARD_ERROR_RQ_RCV = -4;
        public const int ADD_FORWARD_ERROR_RQ_PHONE = -3;
        public const int ADD_FORWARD_ERROR_CODE_2 = -2;
        public const int ADD_FORWARD_ERROR_CODE_1 = -1;
        public const int ADD_FORWARD_UNDEFINED = 0;
        public const int ADD_FORWARD_SUCCESS = 1;

        // PHONE TYPE
        public const int TEMP_SMS = 0;
        public const int CHO_THUE_SIM_CODE = 1;

    }
}
