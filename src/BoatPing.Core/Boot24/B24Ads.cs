//using System;
//using System.Collections.Generic;
//using OpenQA.Selenium.Chrome;
//using Yaapii.Atoms.Enumerable;

//namespace BoatPing.Core.Boot24
//{
//    public sealed class B24Ads : ManyEnvelope<string>
//    {
//        public B24Ads() : base(() =>
//            {
//                using (var driver = new ChromeDriver())
//                {
//                    var result = new ManyOf<string>();
//                    driver.Navigate().GoToUrl("https://www.boot24.com/segelboot/#pab=35000&pbis=58000&jahrvon=1970&lmin=10&lmax=13&land=c0");
//                    result = new Joined<string>(new B24PageAds(driver), result);
//                    return result;
//                }
//            },
//            false
//        )
//        {
//        }
//    }
//}
