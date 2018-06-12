using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using System;

namespace Blind_Alley
{
    [Activity(Label = "Blind_Alley", MainLauncher = true)]
    public class MainActivity : Activity
    {
        View mainView;
        Button forwardBtn;
        Button leftBtn;
        Button rightBtn;
        Button backwardBtn;
        Button interactBtn;
        Player player;
        MapHandler map;
        TextView txtV;

        private void setListeners()
        {
            forwardBtn.Click += (sender, e) =>
            {
                Console.WriteLine("Forward!");
                player.move(true);
            };

            leftBtn.Click += (sender, e) =>
            {
                Console.WriteLine("Turn Left!");
                player.turnLeft();
            };

            interactBtn.Click += (sender, e) =>
            {
                 Console.WriteLine("Wut?");
                player.interact();
            };

            rightBtn.Click += (sender, e) =>
            {
                Console.WriteLine("Turn Right!");
                player.turnRight();
            };

            backwardBtn.Click += (sender, e) =>
            {
                Console.WriteLine("Backward!");
                player.move(false);
            };
        }

        private void generateMap()
        {
            map = new MapHandler(25, 25);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            //SetContentView(Resource.Layout.Main);
            //mainView = Window.DecorView;
            //forwardBtn = mainView.FindViewById<Button>(Resource.Id.buttonForward);
            //leftBtn = mainView.FindViewById<Button>(Resource.Id.buttonLeft);
            //rightBtn = mainView.FindViewById<Button>(Resource.Id.buttonRight);
            //interactBtn = mainView.FindViewById<Button>(Resource.Id.buttonInteract);
            //backwardBtn = mainView.FindViewById<Button>(Resource.Id.buttonBackward);
            player = new Player(0, 0);

            //setListeners();
            //generateMap();

            // Testing map generation
            SetContentView(Resource.Layout.MapDebugging);
            MapHandler map = new MapHandler(25, 25);
            txtV = FindViewById<TextView>(Resource.Id.textView1);
            txtV.Text = map.getMapStr();
            Console.WriteLine(txtV.Text);
        }
    }
}

