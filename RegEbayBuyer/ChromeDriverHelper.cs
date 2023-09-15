using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RegEbayBuyer
{
    public class ChromeDriverHelper
    {
        public static bool QuickAction(ChromeDriver chromeDriver, string nameType, string valueType, int elementOrder, string fillText, int timeout, string action = "CLICK OR SENDKEY")
        {
            for (int i = 0; i < timeout; i++)
            {
                try
                {
                    if (action.Equals("SENDKEY"))
                    {
                        if (nameType.Equals("Id"))
                        {
                            Thread.Sleep(1000);
                            chromeDriver.FindElements(By.Id(valueType))[elementOrder].SendKeys(fillText);
                            return true;
                        }
                        else if (nameType.Equals("ClassName"))
                        {
                            Thread.Sleep(1000);
                            chromeDriver.FindElements(By.ClassName(valueType))[elementOrder].SendKeys(fillText);
                            return true;
                        }
                        else if (nameType.Equals("TagName"))
                        {
                            Thread.Sleep(1000);
                            chromeDriver.FindElements(By.TagName(valueType))[elementOrder].SendKeys(fillText);
                            return true;
                        }
                        else if (nameType.Equals("XPath"))
                        {
                            Thread.Sleep(1000);
                            chromeDriver.FindElements(By.XPath(valueType))[elementOrder].SendKeys(fillText);
                            return true;
                        }
                    }
                    else if (action.Equals("CLICK"))
                    {
                        if (nameType.Equals("Id"))
                        {
                            Thread.Sleep(1000);
                            chromeDriver.FindElements(By.Id(valueType))[elementOrder].Click();
                            return true;
                        }
                        else if (nameType.Equals("ClassName"))
                        {
                            Thread.Sleep(1000);
                            chromeDriver.FindElements(By.ClassName(valueType))[elementOrder].Click();
                            return true;
                        }
                        else if (nameType.Equals("TagName"))
                        {
                            Thread.Sleep(1000);
                            chromeDriver.FindElements(By.TagName(valueType))[elementOrder].Click();
                            return true;
                        }
                        else if (nameType.Equals("XPath"))
                        {
                            Thread.Sleep(1000);
                            chromeDriver.FindElements(By.XPath(valueType))[elementOrder].Click();
                            return true;
                        }
                    }
                }
                catch { }

                Thread.Sleep(1000);
            }

            return false;
        }
    }
}
