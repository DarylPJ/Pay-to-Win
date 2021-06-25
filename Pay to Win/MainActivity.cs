using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Content;
using Android.Media;
using Android.Content.PM;

namespace Pay_to_Win
{
    [Activity(MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {
        bool payToWin = false;
        bool viewWin = false;
        Button WinButton;
        MediaPlayer Player;

        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }

        protected override void OnResume()
        {
            base.OnResume();
            payToWin = false;
            viewWin = false;
            SetContentView(Resource.Layout.Main);
        }


        [Java.Interop.Export("Link_To_Christmas_Countdown")]
        public void Link_To_Christmas_Countdown(View v)
        {
            Intent intent = new Intent(Intent.ActionView);
            intent.SetData(Android.Net.Uri.Parse("https://play.google.com/store/apps/details?id=daryljones.blockwars"));
            StartActivity(intent);
        }

        [Java.Interop.Export("Play_Game")]
        public void Play_Game(View v)
        {
            if (Player!=null)
            {
                Player.Pause();
                Player.Reset();
            }

            if (payToWin && viewWin)
            {
               SetContentView(Resource.Layout.You_Win);
                Player = MediaPlayer.Create(this, Resource.Raw.You_Win);
            }
            else
            {
               SetContentView(Resource.Layout.You_Lose);
                Player = MediaPlayer.Create(this, Resource.Raw.You_Lose_Good_Day_Sir);
                if (payToWin)
                {
                    WinButton = FindViewById<Button>(Resource.Id.buttonPaytoWin);
                    WinButton.Text = "Go to Win";
                }
            }

            Player.Start();
            if (Player.IsPlaying)
            {
                bool frbe = Player.IsPlaying;
            }
        }


        [Java.Interop.Export("Pay_to_win")]
        public void Pay_to_win(View v)
        {
            viewWin = true;
            if (payToWin)
            {
                Play_Game(v);
            }
            else
            {
                SetContentView(Resource.Layout.BuyTransaction);
            }
        }

        [Java.Interop.Export("View_Lose")]
        public void View_Lose(View v)
        {
            viewWin = false;
            Play_Game(v);
        }

        [Java.Interop.Export("Paid_Win")]
        public void Paid_Win(View v)
        {
            payToWin = true;
            Play_Game(v);
        }

    }
}

