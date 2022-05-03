using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Philosophers
{
    public class Philosophers
    {
        public Philosopher[] philosophers;

        private Fork[] forks;

        private PhilosopherGUI mainGUI;

        private object[] locks;

        private bool isRunning;

        public bool IsRunning
        {
            get { return isRunning; }
            set { isRunning = value; }
        }


        public Philosophers()
        {
            isRunning = false;

            mainGUI = new PhilosopherGUI();

          
        }

        public void ActivateDinner()
        {
            Initialize();
            mainGUI.Initialise(this);

            
        }

        //
        //
        //            p0
        //       f0         f1
        //     p4             p1
        //      f4          f2
        //        p3     p2
        //           f3
        //

        /// <summary>
        /// Initializes forks, philosophers and various properties
        /// </summary>
        public void Initialize()
        {
            philosophers = new Philosopher[5];

            forks = new Fork[5];

            locks = new object[5]
            {
                new object(),
                new object(),
                new object(),
                new object(),
                new object(),
            };

            forks[0] = new Fork(locks[0]);
            forks[1] = new Fork(locks[1]);
            forks[2] = new Fork(locks[2]);
            forks[3] = new Fork(locks[3]);
            forks[4] = new Fork(locks[4]);

            forks[0].ID = 0;
            forks[1].ID = 1;
            forks[2].ID = 2;
            forks[3].ID = 3;
            forks[4].ID = 4;


            philosophers[0] = new Philosopher(mainGUI);
            philosophers[0].Fork1 = forks[0];
            philosophers[0].Fork2 = forks[1];
            philosophers[0].TimeToDie = 20000;
            philosophers[0].EatingTime = 4000;
            philosophers[0].ID = 0;
            philosophers[0].MeditationTime = 5000;

            philosophers[1] = new Philosopher(mainGUI);
            philosophers[1].Fork1 = forks[1];
            philosophers[1].Fork2 = forks[2];
            philosophers[1].TimeToDie = 20000;
            philosophers[1].EatingTime = 2000;
            philosophers[1].ID = 1;
            philosophers[1].MeditationTime = 5000;


            philosophers[2] = new Philosopher(mainGUI);
            philosophers[2].Fork1 = forks[2];
            philosophers[2].Fork2 = forks[3];
            philosophers[2].TimeToDie = 20000;
            philosophers[2].EatingTime = 6000;
            philosophers[2].ID = 2;
            philosophers[2].MeditationTime = 5000;


            philosophers[3] = new Philosopher(mainGUI);
            philosophers[3].Fork1 = forks[3];
            philosophers[3].Fork2 = forks[4];
            philosophers[3].TimeToDie = 20000;
            philosophers[3].EatingTime = 3000;
            philosophers[3].ID = 3;
            philosophers[3].MeditationTime = 5000;


            philosophers[4] = new Philosopher(mainGUI);
            philosophers[4].Fork1 = forks[4];
            philosophers[4].Fork2 = forks[0];
            philosophers[4].TimeToDie = 15000;
            philosophers[4].EatingTime = 1000;
            philosophers[4].ID = 4;
            philosophers[4].MeditationTime = 5000;
        }

        /// <summary>
        /// Starts the dinner
        /// </summary>
        public void Start()
        {
            isRunning = true;
            mainGUI.Start();

            for (int i = 0; i < philosophers.Length; i++)
            {
                philosophers[i].StartProcedure();
                Thread.Sleep(100);
            }

            Console.ReadKey();
        }


    }
}
