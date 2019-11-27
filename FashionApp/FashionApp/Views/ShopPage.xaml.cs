using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using org.mariuszgromada.math.mxparser;
using org.mariuszgromada.math;
using CSharpMath.Forms;
using System.ComponentModel;
using System.Threading;

namespace FashionApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public static class getstringbeetween
    {
        public static string GetStringBetween(this string token, string first, string second)
        {
            if (!token.Contains(first)) return "";

            var afterFirst = token.Split(new[] { first }, StringSplitOptions.None)[1];

            if (!afterFirst.Contains(second)) return "";

            var result = afterFirst.Split(new[] { second }, StringSplitOptions.None)[0];

            return result;
        }
    }
    public partial class ShopPage : ContentPage
    {

        public ShopPage()
        {
            InitializeComponent();
            this.BindingContext = this;
        }
        double dolenkoren;
        double ABNadZnamenatel;
        int ABPodZnamenatel;
        string strAB;
        string strBC;
        string strAC;
        double BCNadZnamenatel;
        int BCPodZnamenatel;
        double ACNadZnamenatel;
        int ACPodZnamenatel;
        
        double NamiraneLCM(double LCM1, int PurvoChisloPodLiniq, int VtoroChisloPodLiniq, int TretoChisloPodLiniq)
        {
            Expression namiranenalcm = new Expression("lcm(" + PurvoChisloPodLiniq.ToString() + "," + VtoroChisloPodLiniq.ToString() + "," + TretoChisloPodLiniq + ")");
            LCM1 = namiranenalcm.calculate();






            return LCM1;
        }
        
        double NamiraneLCMSDveChisla(int PurvoChisloPodLiniq, int VtoroChisloPodLiniq)
        {
            Expression namiranenalcm = new Expression("lcm(" + PurvoChisloPodLiniq.ToString() + "," + VtoroChisloPodLiniq.ToString() + ")");
            double LCM1 = namiranenalcm.calculate();






            return LCM1;
        }
        string IzchislenienaZnamenatel(double Obshitiqznamenatel, int PurvoChisloPodLiniq, double PurvoChisloNadLiniq, int VtoriChisloPodLiniq, double VtoroChisloNadLiniq, int TretoChisloPodLiniq, double TretoChisloNadLiniq)
        {
            double KraenRezultat = (((Obshitiqznamenatel / PurvoChisloPodLiniq) * PurvoChisloNadLiniq) + ((Obshitiqznamenatel / VtoriChisloPodLiniq) * VtoroChisloNadLiniq) + ((Obshitiqznamenatel / TretoChisloPodLiniq) * TretoChisloNadLiniq));
            return @"\frac{" + KraenRezultat.ToString() + "}{" + Obshitiqznamenatel.ToString() + "}";
        }
        string IzchislenienaZnamenatelSDveChisla(double Obshitiqznamenatel, int PurvoChisloPodLiniq, double PurvoChisloNadLiniq, int VtoriChisloPodLiniq, double VtoroChisloNadLiniq)
        {
            double KraenRezultat = (((Obshitiqznamenatel / VtoriChisloPodLiniq) * VtoroChisloNadLiniq) - ((Obshitiqznamenatel / PurvoChisloPodLiniq) * PurvoChisloNadLiniq));
            return @"\frac{" + KraenRezultat.ToString() + "}{" + Obshitiqznamenatel.ToString() + "}";
        }
        double IzchislenienaZnamenatelSDveChisla1(double Obshitiqznamenatel, int PurvoChisloPodLiniq, double PurvoChisloNadLiniq, int VtoriChisloPodLiniq, double VtoroChisloNadLiniq)
        {
            double KraenRezultat = (((Obshitiqznamenatel / VtoriChisloPodLiniq) * VtoroChisloNadLiniq) - ((Obshitiqznamenatel / PurvoChisloPodLiniq) * PurvoChisloNadLiniq));
            return KraenRezultat;
        }
        private bool IsDecimal(double inputNum)
        {
            return (int)inputNum != inputNum;
        }
        private void OprostqvaneNaKorena(double pready)
        {
            var view5 = new MathView();
            view5.FontSize = 55;
            double inputNum = pready;

            //Check if it is a perfect square number
            if (!IsDecimal(Math.Sqrt(inputNum)))
            {
                lblOprastqvaneNaKornena.Text = "Опростяваме корена(В случея е точен):";
                view5.LaTeX = "S=" + Math.Sqrt(inputNum).ToString(); //mahame korena
                OprostenoLice.Children.Add(view5);
                return;
            }


            double testSquare = 0;
            for (int i = (int)Math.Floor(Math.Sqrt(inputNum)); i >= 2; i--)
            {
                testSquare = inputNum / (double)(i * i);

                if (!IsDecimal(testSquare))
                {
                    lblOprastqvaneNaKornena.Text = "Опростяваме корена:";
                    view5.LaTeX = @"S=" + i.ToString() + @"\sqrt{" + (inputNum / (i * i)).ToString() + "}";
                    OprostenoLice.Children.Add(view5);
                    return;
                }
            }
            lblOprastqvaneNaKornena.Text = "Опростяваме корена (В случея няма нужда):";
            view5.LaTeX = @"S=\sqrt{" + inputNum.ToString() + "}";
            OprostenoLice.Children.Add(view5);



        }
        private void OprostqvaneNaKorena1(double gorenkoren)
        {
            var view5 = new MathView();
            view5.FontSize = 55;
            double inputNum = gorenkoren;

            //Check if it is a perfect square number
            if (!IsDecimal(Math.Sqrt(inputNum)))
            {
                lblOprastqvaneNaKornena.Text = "Опростяваме корена(В случея е точен):";
                view5.LaTeX = @"\frac{S=" + Math.Sqrt(inputNum).ToString() + "}{" + DolenKoren(dolenkoren) + "}"; //mahame korena
                OprostenoLice.Children.Add(view5);
                return;
            }


            double testSquare = 0;
            for (int i = (int)Math.Floor(Math.Sqrt(inputNum)); i >= 2; i--)
            {
                testSquare = inputNum / (double)(i * i);

                if (!IsDecimal(testSquare))
                {
                    lblOprastqvaneNaKornena.Text = "Опростяваме корена:";
                    view5.LaTeX = @"S=" + @"\frac{" + i.ToString() + @"\sqrt{" + (inputNum / (i * i)).ToString() + "}" + "}{" + DolenKoren(dolenkoren) + "}";
                    OprostenoLice.Children.Add(view5);
                    return;
                }
            }
            lblOprastqvaneNaKornena.Text = "Опростяваме корена (В случея няма нужда):";
            view5.LaTeX = @"S=" + @"\frac{" + @"\sqrt{" + inputNum.ToString() + "}" + "}{" + DolenKoren(dolenkoren) + "}";
            OprostenoLice.Children.Add(view5);



        }
        private string DolenKoren(double dolenkoren)
        {
            double inputNum = dolenkoren;

            //Check if it is a perfect square number
            if (!IsDecimal(Math.Sqrt(inputNum)))
            {
                lblOprastqvaneNaKornena.Text = "Опростяваме корена(В случея е точен):";
                string view6 = Math.Sqrt(inputNum).ToString(); //mahame korena
                return view6;
            }


            double testSquare = 0;
            for (int i = (int)Math.Floor(Math.Sqrt(inputNum)); i >= 2; i--)
            {
                testSquare = inputNum / (double)(i * i);

                if (!IsDecimal(testSquare))
                {
                    lblOprastqvaneNaKornena.Text = "Опростяваме корена:";
                    string view5 = i.ToString() + @"\sqrt{" + (inputNum / (i * i)).ToString() + "}";
                    return view5;
                }
            }
            lblOprastqvaneNaKornena.Text = "Опростяваме корена (В случея няма нужда):";
            string view7 = @"\sqrt{" + inputNum.ToString() + "}";
            return view7;
        }

        [Obsolete]

        private async void MathSolve_Clicked(object sender, EventArgs e)
        {

            await Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await DoWork();

                    await PopupNavigation.PushAsync(new PopUpSuccess());

                    MessagingCenter.Send<ShopPage>(this, "Hi");


                });

            });


           

           


        }


        public async Task DoWork()
        {
            try
            {

               
                


                Expression PGcd1 = new Expression("lcm(" + BCPodZnamenatel.ToString() + "," + ACPodZnamenatel.ToString() + "," + ABPodZnamenatel.ToString() + ")");
                double Pgcd = PGcd1.calculate();

                var view = new MathView();
                view.FontSize = 55;
                view.HorizontalOptions = view.VerticalOptions = LayoutOptions.FillAndExpand;
                view.LaTeX = @"P=a+b+c";
                var view2 = new MathView();
                var Preshenie1 = new MathView();
                Preshenie1.FontSize = 55;
                if (ABPodZnamenatel == 1 && BCPodZnamenatel == 1 && ACPodZnamenatel == 1)
                {
                    view2.LaTeX = @"P=\frac{" + ABNadZnamenatel.ToString() + "}" + "{" + ABPodZnamenatel.ToString() + "} + " + @"\frac{ " + BCNadZnamenatel.ToString() + "} " + "{ " + BCPodZnamenatel.ToString() + "} + " + @"\frac{ " + ACNadZnamenatel.ToString() + "} " + "{ " + ACPodZnamenatel.ToString() + "}=" + @"\frac{ " + (ABNadZnamenatel + BCNadZnamenatel + ACNadZnamenatel).ToString() + "} " + "{1}";

                }
                else
                {
                    view2.LaTeX = @"P=\frac{" + ABNadZnamenatel.ToString() + "}" + "{" + ABPodZnamenatel.ToString() + "} + " + @"\frac{ " + BCNadZnamenatel.ToString() + "} " + "{ " + BCPodZnamenatel.ToString() + "} + " + @"\frac{ " + ACNadZnamenatel.ToString() + "} " + "{ " + ACPodZnamenatel.ToString() + "}=" + @"\frac{" + (((Pgcd / ABPodZnamenatel) * ABNadZnamenatel) + ((Pgcd / ACPodZnamenatel) * ACNadZnamenatel) + ((Pgcd / BCPodZnamenatel) * BCNadZnamenatel)).ToString() + "}{" + Pgcd.ToString() + "}";
                }


                view2.FontSize = 55;
                var view3 = new MathView();
                view3.FontSize = 55;
                var view4 = new MathView();
                view4.FontSize = 55;

                view3.LaTeX = @"S=\sqrt{p(p-a)(p-b)(p-c)}";
                double p = (ABNadZnamenatel + BCNadZnamenatel + ACNadZnamenatel);
                if (ABPodZnamenatel == 1 && BCPodZnamenatel == 1 && ACPodZnamenatel == 1 && p % 2 == 0)
                {
                    p = p / 2;
                    view3.LaTeX = @"S=\sqrt{" + p.ToString() + "(" + p.ToString() + "-" + ABNadZnamenatel.ToString() + ")" + "(" + p.ToString() + "-" + BCNadZnamenatel.ToString() + ")" + "(" + p.ToString() + "-" + ACNadZnamenatel.ToString() + ")" + "}";
                    view4.LaTeX = @"S=\sqrt{" + p.ToString() + "(" + (p - ABNadZnamenatel).ToString() + ")" + "(" + (p - BCNadZnamenatel).ToString() + ")" + "(" + (p - ACNadZnamenatel).ToString() + ")" + "}=" + @"\sqrt{" + (p * (p - ABNadZnamenatel) * (p - ACNadZnamenatel) * (p - BCNadZnamenatel)).ToString() + "}";
                    double pready = (p * (p - ABNadZnamenatel) * (p - ACNadZnamenatel) * (p - BCNadZnamenatel));
                    OprostqvaneNaKorena(pready);
                }
              //  else
             //   {


                  //  view3.LaTeX = @"S=\sqrt{" + @"\frac{" + p.ToString() + "}{2}" + "(" + @"\frac{" + p.ToString() + "}{2}" + "-" + @"\frac{" + ABNadZnamenatel.ToString() + "}{" + ABPodZnamenatel.ToString() + "}" + ")" + "(" + @"\frac{" + p.ToString() + "}{2}" + "-" + @"\frac{" + BCNadZnamenatel.ToString() + "}{" + BCPodZnamenatel.ToString() + "}" + ")" + "(" + @"\frac{" + p.ToString() + "}{2}" + "-" + @"\frac{" + ACNadZnamenatel.ToString() + "}" + "{" + ACPodZnamenatel.ToString() + "}" + ")" + "}";

                 //   double namiraneMe = p * (IzchislenienaZnamenatelSDveChisla1(NamiraneLCMSDveChisla(2, ABPodZnamenatel), ABPodZnamenatel, ABNadZnamenatel, 2, p)) * (IzchislenienaZnamenatelSDveChisla1(NamiraneLCMSDveChisla(2, BCPodZnamenatel), BCPodZnamenatel, BCNadZnamenatel, 2, p)) * (IzchislenienaZnamenatelSDveChisla1(NamiraneLCMSDveChisla(2, ACPodZnamenatel), ACPodZnamenatel, ACNadZnamenatel, 2, p));
                 //   double gorenkoren = namiraneMe;
                 //   dolenkoren = NamiraneLCMSDveChisla(2, ABPodZnamenatel) * NamiraneLCMSDveChisla(2, BCPodZnamenatel) * NamiraneLCMSDveChisla(2, ACPodZnamenatel) * 2;
                 //    view4.LaTeX = @"S=\sqrt{" + @"\frac{" + p.ToString() + "}{2}" + "(" + (IzchislenienaZnamenatelSDveChisla(NamiraneLCMSDveChisla(2, ABPodZnamenatel), ABPodZnamenatel, ABNadZnamenatel, 2, p)).ToString() + ")" + "(" + (IzchislenienaZnamenatelSDveChisla(NamiraneLCMSDveChisla(2, BCPodZnamenatel), BCPodZnamenatel, BCNadZnamenatel, 2, p)).ToString() + ")" + "(" + (IzchislenienaZnamenatelSDveChisla(NamiraneLCMSDveChisla(2, ACPodZnamenatel), ACPodZnamenatel, ACNadZnamenatel, 2, p)).ToString() + ")" + "}=" + @"\sqrt{" + @"\frac{" + namiraneMe.ToString() + "}{" + NamiraneLCMSDveChisla(2, ABPodZnamenatel) * NamiraneLCMSDveChisla(2, BCPodZnamenatel) * NamiraneLCMSDveChisla(2, ACPodZnamenatel) * 2 + "}" + "}";
                //    OprostqvaneNaKorena1(gorenkoren);
              //  }
               

                    GridTomath.Children.Add(view);
                    GridP2.Children.Add(view2);
                    SReshenie.Children.Add(view3);
                    Sreshenie2.Children.Add(view4);
                
            }
            catch (Exception ex)
            {
                await DisplayAlert("", "resh", "OK");
            }
        }

        private void TBMathProblem_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                strAB = TBMathProblem.Text.GetStringBetween("AB=", "cm");
                strBC = TBMathProblem.Text.GetStringBetween("BC=", "cm");
                strAC = TBMathProblem.Text.GetStringBetween("AC=", "cm");
                if (!(strAB.Contains("/")))
                {
                    strAB += "/1";
                }
                if (!strAC.Contains("/"))
                {
                    strAC += "/1";
                }
                if (!strBC.Contains("/"))
                {
                    strBC += "/1";
                }
                ABNadZnamenatel = double.Parse(strAB.Split('/')[0]);
                ABPodZnamenatel = int.Parse(strAB.Split('/')[1]);
                BCNadZnamenatel = double.Parse(strBC.Split('/')[0]);
                BCPodZnamenatel = int.Parse(strBC.Split('/')[1]);
                ACNadZnamenatel = double.Parse(strAC.Split('/')[0]);
                ACPodZnamenatel = int.Parse(strAC.Split('/')[1]);
            }
            catch
            {

            }
        }
    }
}