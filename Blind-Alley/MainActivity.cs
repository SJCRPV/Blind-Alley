using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using System;
using System.Threading;
using Plugin.SimpleAudioPlayer;
using SQLite;
using System.IO;
using Android.Graphics;
using Android.Content.Res;

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
        TextView areaDescription;
        TextView txtV;

        ISimpleAudioPlayer ambientSound;
        ISimpleAudioPlayer stickSound;
        ISimpleAudioPlayer minotaurDistantSound;
        ISimpleAudioPlayer minotaurDistant2Sound;
        ISimpleAudioPlayer minotaurNearbySound;
        ISimpleAudioPlayer minotaurDiscoveredSound;
        ISimpleAudioPlayer footstepSound;
        ISimpleAudioPlayer victorySound;
        ISimpleAudioPlayer deathSound;

        SQLiteConnection database;

        Color baseBtnColour;
        double initialDistToObjective;
        double initialDistToMonster;

        private double calcDistance(int[] leftCoords, int[] rightCoords)
        {
            return Math.Sqrt(Math.Pow(leftCoords[0] - rightCoords[0], 2) + Math.Pow(leftCoords[1] - rightCoords[1], 2));
        }

        private void setDB()
        {
            database = new SQLiteConnection(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "Highscore.db3"));
            database.CreateTable<Highscore>();
        }

        private void setSounds()
        {
            ambientSound = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            ambientSound.Load("sounds/Ambient.wav");
            ambientSound.Play();
            ambientSound.Loop = true;

            stickSound = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            stickSound.Load("sounds/HitTheGround.wav");

            minotaurDistantSound = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            minotaurDistantSound.Load("sounds/Distant.wav");

            //minotaurDistant2Sound = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            //minotaurDistant2Sound.Load("sounds/Distant2.wav");

            //minotaurNearbySound = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            //minotaurNearbySound.Load("sounds/Nearby.wav");

            minotaurDiscoveredSound = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            minotaurDiscoveredSound.Load("sounds/Discovered.wav");

            footstepSound = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            footstepSound.Load("sounds/Footstep.wav");

            victorySound = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            victorySound.Load("sounds/Victory.wav");

            deathSound = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            deathSound.Load("sounds/Death.wav");
        }

        private void blockButtons()
        {
            forwardBtn.Enabled = false;
            leftBtn.Enabled = false;
            rightBtn.Enabled = false;
            backwardBtn.Enabled = false;
            interactBtn.Enabled = false;
        }

        private bool checkIfItGetsInserted(int score)
        {
            if(database.Get<int>(1) < score)
            {
                database.DeleteAll<int>();
                database.Insert(score);
                return true;
            }

            return false;
        }

        private void setTriggers()
        {
            TimeSpan startTime = TimeSpan.Zero;
            TimeSpan delayBetweenMovement = TimeSpan.FromSeconds(1);
            initialDistToObjective = calcDistance(Player.PlayerCoords, Map.ObjectiveCoords);
            initialDistToMonster = calcDistance(Player.PlayerCoords, Monster.MonsterCoords);

            Timer timer = new Timer((e) =>
            {
                if (Player.PlayerCoords.Equals(Monster.MonsterCoords))
                {
                    blockButtons();

                    if (!deathSound.IsPlaying)
                    {
                        deathSound.Play();
                    }
                    int score = TimeSpan.Compare(TimeSpan.Zero, TimeSpan.Parse(DateTime.Now.ToString()));
                    areaDescription.Text = "The Minotaur kills you\nGame Over\nYou lasted for " + TimeSpan.FromMinutes(score) + " minutes.";
                    if(checkIfItGetsInserted(score))
                    {
                        areaDescription.Text += "\nIt's a highscore, too!";
                    }
                }

                if (Player.PlayerCoords.Equals(Map.ObjectiveCoords))
                {
                    blockButtons();

                    if(!victorySound.IsPlaying)
                    {
                        victorySound.Play();
                    }
                    int score = TimeSpan.Compare(TimeSpan.Zero, TimeSpan.Parse(DateTime.Now.ToString()));
                    areaDescription.Text = "You found the chest! Your vision is restored!\nIt took you " + TimeSpan.FromMinutes(score) + " minutes.";
                    if (checkIfItGetsInserted(score))
                    {
                        areaDescription.Text += "\nIt's a highscore, too!";
                    }
                }

                RunOnUiThread(() =>
                {
                    double currDistToPlace = calcDistance(Player.PlayerCoords, Map.ObjectiveCoords);
                    double ratio = currDistToPlace / initialDistToObjective;
                    int r = (int)(baseBtnColour.R + ((255 - baseBtnColour.R) * (1 - ratio)));
                    int g = baseBtnColour.G;
                    int b = baseBtnColour.B;
                    Color nBtnColour = new Color(r, g, b);
                    forwardBtn.Background.SetColorFilter(nBtnColour, PorterDuff.Mode.Src);
                    leftBtn.Background.SetColorFilter(nBtnColour, PorterDuff.Mode.Src);
                    rightBtn.Background.SetColorFilter(nBtnColour, PorterDuff.Mode.Src);
                    backwardBtn.Background.SetColorFilter(nBtnColour, PorterDuff.Mode.Src);
                    interactBtn.Background.SetColorFilter(nBtnColour, PorterDuff.Mode.Src);

                    currDistToPlace = calcDistance(Player.PlayerCoords, Monster.MonsterCoords);
                    ratio = currDistToPlace / initialDistToObjective;
                    if(ratio < 0.5 && !minotaurDistantSound.IsPlaying)
                    {
                        minotaurDistantSound.Volume = 100 * (1 - ratio);
                        minotaurDistantSound.Play();
                    }
                });


            }, null, startTime, delayBetweenMovement);
        }

        private void setListeners()
        {
            forwardBtn.Click += (sender, e) =>
            {
                Console.WriteLine("Forward!");
                footstepSound.Play();
                player.move(true);
            };

            leftBtn.Click += (sender, e) =>
            {
                Console.WriteLine("Turn Left!");
                footstepSound.Play();
                player.turnLeft();
            };

            interactBtn.Click += (sender, e) =>
            {
                Console.WriteLine("Looking with my special eyes");
                stickSound.Play();
                areaDescription.Text = player.interact();
            };

            rightBtn.Click += (sender, e) =>
            {
                Console.WriteLine("Turn Right!");
                footstepSound.Play();
                player.turnRight();
            };

            backwardBtn.Click += (sender, e) =>
            {
                Console.WriteLine("Backward!");
                footstepSound.Play();
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
            SetContentView(Resource.Layout.Main);
            mainView = Window.DecorView;
            forwardBtn = mainView.FindViewById<Button>(Resource.Id.buttonForward);
            leftBtn = mainView.FindViewById<Button>(Resource.Id.buttonLeft);
            rightBtn = mainView.FindViewById<Button>(Resource.Id.buttonRight);
            interactBtn = mainView.FindViewById<Button>(Resource.Id.buttonInteract);
            backwardBtn = mainView.FindViewById<Button>(Resource.Id.buttonBackward);
            areaDescription = mainView.FindViewById<TextView>(Resource.Id.areaDescription);
            baseBtnColour = Color.Gray;

            player = new Player(0, 0);

            setDB();
            setSounds();
            setListeners();
            generateMap();
            setTriggers();

            // Testing map generation
            //SetContentView(Resource.Layout.MapDebugging);
            //MapHandler map = new MapHandler(25, 25);
            //txtV = FindViewById<TextView>(Resource.Id.textView1);
            //txtV.Text = map.getMapStr();
            //Console.WriteLine(txtV.Text);
            map.moveMonster();
        }
    }
}

