using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

/*  FIREBASE
 * var config = {
    apiKey: "AIzaSyBKk-NKjbE07HfhqWY1Go-mezRrz6w5o5A",
    authDomain: "blazing-torch-7475.firebaseapp.com",
    databaseURL: "https://blazing-torch-7475.firebaseio.com",
    storageBucket: "blazing-torch-7475.appspot.com",
  };
 * */

namespace ConsoleApplication1
{
    public class Program
    {
        static void Main(string[] args)
        {
            ////var request = (HttpWebRequest)WebRequest.Create("https://blazing-torch-7475.firebaseio.com/");

            //IFirebaseConfig config = new FirebaseConfig
            //{
            //    AuthSecret = "your_firebase_secret",
            //    BasePath = "https://blazing-torch-7475.firebaseio.com/"
            //};


            Console.WriteLine("Before");
            YellowConstructor x = new YellowConstructor();
            x.GetMenu();
            Console.WriteLine("After");
            Console.ReadKey();
        }
    }
}