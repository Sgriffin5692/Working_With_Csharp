using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

/*
    Created by: Seth Griffin
    11/15/2018
    Homework 8 for CIS466
*/

namespace Homework8_SethGriffin
{
    class Program
    {
        public static void Main(string[] args)
        {
            string result;

            do
            {
                result = DisplayMenu();
                Run(result);

            } while (result != "5");

            Console.WriteLine(" Good Bye...");

        }

        public static string DisplayMenu()
        {

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Homework 8");
            Console.WriteLine("");
            Console.WriteLine("Hit [1] to run Exercise 10_8 (Debugging).");
            Console.WriteLine("Hit [2] to run Exercise 10_9 (BankAccount).");
            Console.WriteLine("Hit [3] to run Exercise 10_13 (Restaurant).");
            Console.WriteLine("Hit [4] to run Exercise 10_17 (Shape).");

            Console.WriteLine("");
            Console.WriteLine("Hit [5]: Exit;");
            Console.WriteLine("");
            Console.WriteLine("");

            var result = Console.ReadLine();
            return result;
        }

        private static bool Run(string exeArg)
        {
            switch (exeArg.ToLower())
            {
                case "1":
                    Application.Run(new Ex10_8());
                    return true;
                case "2":
                    RunEx10_9();
                    return true;
                case "3":
                    RunEx10_13();
                    return true;
                case "4":
                    Application.Run(new Ex10_17());
                    return true;
                default:
                    Console.WriteLine("Exiting the Program!");
                    return true;

            }
        }
        public static void RunEx10_9()
        {
            SavingsAccount savings = new SavingsAccount(100.00, 3.5);
            SavingsAccount s = (SavingsAccount)savings.ReadAccount();
            CheckingAccount checking = new CheckingAccount(1000.00, .50);
            CheckingAccount c = (CheckingAccount)checking.ReadAccount();
            s.Deposit(135.22);
            s.PostInterest();
            s.Withdraw(50);
            Console.WriteLine
            ("The balance of SavingsAccount s is {0:C}",
                s.GetBalance());
            c.Deposit(100.00);
            c.ProcessCheck(200.00);
            c.Withdraw(100.00);
            Console.WriteLine
            ("The balance of CheckingAccount c is {0:C}",
                c.GetBalance());
            Console.ReadLine();
        }

        public static void RunEx10_13()
        {
            FastFood krystal = new FastFood("Krystal");
            CoffeeShop starbucks = new CoffeeShop("Starbucks");
            Fancy snooty = new Fancy("Snooty Grill");
            krystal.EatOut();
            starbucks.EatOut();
            snooty.EatOut();
        }
        
        #region Ex10_8
        public class Oval : Shape
        {
            int xdiam;
            int ydiam;

            // Needed to pass Point object to Shape constructor to assign to location field
            // Added :base(p) to constructor header
            public Oval(Point p, int ma, int mi) : base(p)
            {
                xdiam = ma;
                ydiam = mi;
            }
            public override void Draw(Graphics g)
            {
                // Need to subtract radius (diameter/2), rather than multiply diameter by 2
                // Changed from location.(X/Y)-2*(xdiam/ydiam)
                // to location.(X/Y)-(xdiam/ydiam)/2 
                g.FillEllipse(Brushes.Red, location.X - xdiam / 2,
                     location.Y - ydiam / 2, xdiam, ydiam);
            }
        }
        public class Ex10_8 : Form
        {
            Shape s;

            public Ex10_8()
            {
                Size = new Size(300, 200);
                s = new Oval(new Point(100, 100), 70, 30);
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                s.Draw(g);
                base.OnPaint(e);
            }

            // Cannot have more than one entry point to application
            //static void Main()
            //{
            //    Application.Run(new Ex10_8());
            //}
        }
        #endregion

        #region Ex10_9
        public class BankAccount
        {
            private double balance;

            public BankAccount()
            {
                balance = 0;
            }

            public BankAccount(double initialAmount)
            {
                balance = initialAmount;
            }

            public void Deposit(double amount)
            {
                balance += amount;
            }

            public virtual void Withdraw(double amount)
            {
                if (balance >= amount)
                    balance -= amount;
                else
                    Console.WriteLine("Insufficient funds");
            }

            // Overloaded withdraw method for processing overdrafted checks
            public virtual void Withdraw(double amount, double charge)
            {
                balance -= amount + charge;
            }

            public double GetBalance()
            {
                return balance;
            }

            public virtual BankAccount ReadAccount()
            {
                Console.Write("Initial deposit for the account: ");
                double _initialAmount = double.Parse(Console.ReadLine());
                BankAccount theBankingAccount = new BankAccount(_initialAmount);
                return theBankingAccount;
            }
        }

        public class SavingsAccount : BankAccount
        {
            private double interestRate = 0;

            public SavingsAccount() { }

            public SavingsAccount(double _initialDeposit, double _interestRate):base(_initialDeposit)
            {
                // Converts from percentage to decimal
                interestRate = _interestRate / 100;
            }

            public void PostInterest()
            {
                double balance = GetBalance();
                double interest = balance * interestRate;
                Deposit(interest);
            }

            public override BankAccount ReadAccount()
            {
                Console.Write("Initial deposit for the savings account: ");
                double _initialDeposit = double.Parse(Console.ReadLine());
                Console.Write("Interest rate for the savings account: ");
                double _interestRate = double.Parse(Console.ReadLine());
                SavingsAccount theSavingsAccount = new SavingsAccount(_initialDeposit, _interestRate);
                return theSavingsAccount;
            }
        }

        public class CheckingAccount : BankAccount
        {
            private double overdraftCharge = 0;

            public CheckingAccount() { }

            public CheckingAccount(double _initialDeposit, double _overdraftCharge):base(_initialDeposit)
            {
                overdraftCharge = _overdraftCharge;
            }

            public void ProcessCheck(double amount)
            {
                // Checks if check amount exceeds account balance
                if (GetBalance() >= amount)
                {
                    Withdraw(amount);
                }
                else
                {
                    Console.WriteLine($"You have overdrafted and been charged a ${overdraftCharge} fee.");
                    Withdraw(amount, overdraftCharge);
                }
            }

            public override BankAccount ReadAccount()
            {
                Console.Write("Initial deposit for the checking account: ");
                double _initialDeposit = double.Parse(Console.ReadLine());
                Console.Write("Overdraft charge for the checking account: ");
                double _interestRate = double.Parse(Console.ReadLine());
                CheckingAccount theCheckingAccount = new CheckingAccount(_initialDeposit, _interestRate);
                return theCheckingAccount;
            }
        }
        #endregion

        #region Ex10_13
        public abstract class Restaurant
        {
            // protected allows child classes to access
            protected string name;
            protected double orderTotal = 0;

            public Restaurant()
            {
                name = "nameless";
            }

            public Restaurant(string _name)
            {
                name = _name;
            }

            // Abstract forces child classes to implement (as opposed to virtual)
            public abstract void GetMenu();

            public abstract void GetBill();

            public abstract void OrderFood();

            public void PayBill()
            {
                orderTotal = 0;
                Console.WriteLine($"Bill paid, {orderTotal:c2} owed.");
                Console.ReadLine();
            }

            public void EatOut()
            {
                GetMenu();
                OrderFood();
                GetBill();
                PayBill();
            }
        }

        public class FastFood:Restaurant
        {
            private const double HAMBURGER_COST = 3.5;
            private const double FRY_COST = 2.0;
            private const double COKE_COST = 1.2;

            public FastFood(string _name) : base(_name){}

            public FastFood() : base(){}

            public override void GetMenu()
            {
                Console.WriteLine("**************************\n");
                Console.WriteLine($"Welcome to {name} (fast food), please look at our posted menu.\n");
                Console.WriteLine($"Menu:\nHamburger: {HAMBURGER_COST:c2}\nFries: {FRY_COST:c2}\nCoke: {COKE_COST:c2}");
                Console.ReadLine();
                Console.WriteLine("**************************");
            }

            public override void OrderFood()
            {
                Console.WriteLine("\nPlacing order at the drive thru window.");
                Console.Write("How many hamburgers? ");
                orderTotal += double.Parse(Console.ReadLine()) * HAMBURGER_COST;
                Console.Write("How many fries? ");
                orderTotal += double.Parse(Console.ReadLine()) * FRY_COST;
                Console.Write("How many cokes? ");
                orderTotal += double.Parse(Console.ReadLine()) * COKE_COST;
            }

            public override void GetBill()
            {
                Console.WriteLine();
                Console.WriteLine("**************************");
                Console.WriteLine();
                Console.WriteLine($"You owe {orderTotal:c2}.");
                Console.WriteLine("Customer hands debit card through the drive thru window.");
                Console.ReadLine();
            }
        }

        public class CoffeeShop:Restaurant
        {
            private const double LATTE_COST = 3.5;
            private const double CAPPUCCINO_COST = 4.0;
            private const double COFFEE_COST = 2.5;

            public CoffeeShop(string _name) : base(_name) { }

            public CoffeeShop() : base() { }

            public override void GetMenu()
            {
                Console.WriteLine("**************************\n");
                Console.WriteLine($"Welcome to {name} (coffee shop), please look at our chalkboard menu.\n");
                Console.WriteLine($"Menu:\nCoffee: {COFFEE_COST:c2}\nLatte: {LATTE_COST:c2}\nCappuccino: {CAPPUCCINO_COST:c2}");
                Console.ReadLine();
                Console.WriteLine("**************************");
            }

            public override void OrderFood()
            {
                Console.WriteLine("\nPlacing order at the counter.");
                Console.Write("How many coffees? ");
                orderTotal += double.Parse(Console.ReadLine()) * COFFEE_COST;
                Console.Write("How many lattes? ");
                orderTotal += double.Parse(Console.ReadLine()) * LATTE_COST;
                Console.Write("How many cappuccinos? ");
                orderTotal += double.Parse(Console.ReadLine()) * CAPPUCCINO_COST;
            }

            public override void GetBill()
            {
                Console.WriteLine();
                Console.WriteLine("**************************");
                Console.WriteLine();
                Console.WriteLine($"You owe {orderTotal:c2}.");
                Console.WriteLine("Customer taps smart phone to credit card reader using Samsung Pay.");
                Console.ReadLine();
            }
        }

        public class Fancy:Restaurant
        {
            private const double CAVIAR_COST = 85.0;
            private const double ESCARGOT_COST = 75.0;
            private const double RISOTTO_COST = 55.0;

            public Fancy(string _name) : base(_name) { }

            public Fancy() : base() { }

            public override void GetMenu()
            {
                Console.WriteLine("**************************\n");
                Console.WriteLine($"Welcome to {name} (fancy) sir, please look at our fancy menu.\n");
                Console.WriteLine($"Menu:\nFish Eggs: {CAVIAR_COST:c2}\nDelicious Snails: {ESCARGOT_COST:c2}\nRisotto: {RISOTTO_COST:c2}");
                Console.ReadLine();
                Console.WriteLine("**************************");
            }

            public override void OrderFood()
            {
                Console.WriteLine("\nProviding order to Mr.Fancy-pants waiter.");
                Console.Write("How many orders of caviar? ");
                orderTotal += double.Parse(Console.ReadLine()) * CAVIAR_COST;
                Console.Write("How many orders of escargot? ");
                orderTotal += double.Parse(Console.ReadLine()) * ESCARGOT_COST;
                Console.Write("How many orders of risotto? ");
                orderTotal += double.Parse(Console.ReadLine()) * RISOTTO_COST;
            }

            public override void GetBill()
            {
                Console.WriteLine();
                Console.WriteLine("**************************");
                Console.WriteLine();
                Console.WriteLine($"You owe {orderTotal:c2}.");
                Console.WriteLine("Customer provides waiter with platinum credit card for payment.");
                Console.ReadLine();
            }

        }
        #endregion

        #region Ex10_17
        public class Picture : Shape
        {
            private List<Shape> pic = new List<Shape>();
            private int width;
            private int height;

            // Passes Point object to Shape constructor to assign to location field
            public Picture(Point _p, int _width, int _height):base(_p)
            {
                width = _width;
                height = _height;
            }

            public override void Draw(Graphics g)
            {
                Pen blue = new Pen(Color.Blue, 1);
                g.DrawRectangle(blue, location.X, location.Y, width, height);

                // Draws all objects in this.pic list
                for (int i = 0; i < pic.Count; i++)
                {
                    pic[i].Draw(g);
                }
            }

            public override string ToString()
            {
                string s = "Picture at center " + base.ToString() + "\n";

                // Displays ToString() for all Shape objects in this.pic list
                // Displays adjusted positions, rather than keeping original positions
                for (int i = 0; i < pic.Count; i++)
                {
                    s += pic[i].ToString() + "\n";
                }
                return s;
            }

            public void Add(Shape _shape)
            {
                // Adjusts shape location relative to picture center
                _shape.Move(location.X, location.Y);
                pic.Add(_shape);
            }

            public override void Move(int xamount, int yamount)
            {
                base.Move(xamount, yamount);

                // Shifts shapes relative to picture movement
                for (int i = 0; i < pic.Count; i++)
                {
                    pic[i].Move(xamount, yamount);
                }
            }
        }

        public class Ex10_17 : Form
        {
            Shape s0 = new Line(10, 10, 20, 30);
            Shape s1 = new Circle(new Point(50, 50), 30);
            Shape s2 = new Circle(new Point(150, 150), 30);
            Picture p1 = new Picture(new Point(20, 40), 80, 80);
            Picture p2 = new Picture(new Point(120, 50), 200, 200);
            Shape t0 = new Line(10, 10, 20, 30);
            Shape t1 = new Circle(new Point(50, 50), 30);
            Picture p3 = new Picture(new Point(20, 40), 80, 80);

            public Ex10_17()
            {
                Size = new Size(700, 620);
                p1.Add(s0);
                p1.Add(s1);
                p2.Add(p1);
                p2.Add(s2);
                p2.Move(20, 50);
                p3.Add(t0);
                p3.Add(t1);
                Console.WriteLine(p2);
            }

            // Needed to display picture objects
            // No need to call p1.Draw() due to p1 possessing p2
            protected override void OnPaint(PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                p2.Draw(g);
                p3.Draw(g);
                base.OnPaint(e);
            }
        }
        #endregion
    }
}
