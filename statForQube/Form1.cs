using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace statForQube
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void buttonForAction_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int i, j, N, X;
            double P, E = 0, D = 0, Eg = 0, Dg = 0, Es = 0, Ds = 0, Chi = 0, ChiP = 16.812;
            chartForStat.Series[0].Points.Clear();
            X = (int)prob2.Value;
            double[] possibilites = new double[X + 1];
            int[] events = new int[X + 1];
            possibilites[0] = (double)prob1.Value;
            P = possibilites[0];
            if (possibilites[0] >= 1)
            {
                MessageBox.Show("Вероятность не должна быть >= 1!!!");
            }
            else if (X > 10000)
            {
                MessageBox.Show("Слишком большой Х!!!");
            }
            else
            {
                for (i = 0; i <= X; i++)
                {
                    events[i] = 0;
                }
                for (i = 1; i < X; i++)
                {
                    possibilites[i] = P * Math.Pow((1 - P), i);
                }
                double Pn = possibilites[X - 1];
                double sumOfP = 0;
                for (i = X; i < 10000; i++)
                {
                    sumOfP += P * Math.Pow((1 - P), i);
                }
                if (sumOfP > Pn)
                {
                    MessageBox.Show("Укажите Х побольше");
                }
                else
                {
                    possibilites[X] = sumOfP;
                    N = (int)numOfExp.Value;
                    for (i = 0; i < N; i++)
                    {
                        double b = rnd.NextDouble() % 1;
                        for (j = 0; j <= X; j++)
                        {
                            b -= possibilites[j];
                            if (b <= 0)
                            {
                                events[j]++;
                                break;
                            }
                        }

                    }
                    for (i = 0; i <= X; i++)
                    {
                        P = (double)events[i] / (double)N;
                        chartForStat.Series[0].Points.AddXY(i + 1, P);
                        E += (double)(possibilites[i] * (i + 1));
                        D += (double)(possibilites[i] * (i + 1) * (i + 1));
                        Eg += (double)(P * (i + 1));
                        Dg += (double)(P * (i + 1) * (i + 1));
                        Chi += (double)((events[i] * events[i]) / (N * possibilites[i]));
                    }
                    D -= (double)(E * E);
                    Dg -= (double)(Eg * Eg);
                    Es = (double)((Math.Abs(Eg - E) / Math.Abs(E)) * 100);
                    Ds = (double)((Math.Abs(Dg - D) / Math.Abs(D)) * 100);
                    Chi -= (double)N;
                    label14.Text = "Average:" + Eg + " (error:" + Es + "%)";
                    label15.Text = "Variance:" + Dg + " (error:" + Ds + "%)";
                    if (Chi > ChiP)
                    {
                        label16.Text = "Chi-squared:" + Chi + " > " + ChiP + " is true";
                    }
                    else
                    {
                        label16.Text = "Chi-squared:" + Chi + " <= " + ChiP + " is false";
                    }
                }
            }
        }
    }
}
