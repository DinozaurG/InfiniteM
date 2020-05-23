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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void buttonForAction_Click(object sender, EventArgs e)
        {
            double[] possibilites = new double[6];
            int[] events = new int[6] {0, 0, 0, 0, 0, 0};
            String[] ans = new String[6];
            Random rnd = new Random();
            int i, j, N;
            double P, E = 0, D = 0, Eg = 0, Dg = 0, Es = 0, Ds = 0, Chi = 0, ChiP = 16.812;
            chartForStat.Series[0].Points.Clear();
            possibilites[0] = (double)prob1.Value;
            possibilites[1] = (double)prob2.Value;
            possibilites[2] = (double)prob3.Value;
            possibilites[3] = (double)prob4.Value;
            possibilites[4] = (double)prob5.Value;
            P = possibilites[0] + possibilites[1] + possibilites[2] + possibilites[3] + possibilites[4];
            if (possibilites[0] + possibilites[1] + possibilites[2] + possibilites[3] + possibilites[4] > 1)
            {
                MessageBox.Show("Сумма вероятностей не должна быть больше 1!!!");
            }
            else
            {
                possibilites[5] = (double)(1 - P);
                labelFor6.Text = possibilites[5].ToString();
                N = (int)numOfExp.Value;
                for (i = 0; i < N; i++)
                {
                    double b = rnd.NextDouble() % 1;
                    for (j = 0; j < 6; j++)
                    {
                        b -= possibilites[j];
                        if (b <= 0)
                        {
                            events[j]++;
                            break;
                        }
                    }
                    
                }
                for (i = 0; i < 6; i++)
                {
                    P = (double)events[i] / (double)N;
                    chartForStat.Series[0].Points.AddXY(i + 1, P);
                    E += (double)(possibilites[i] * (i + 1));
                    D += (double)(possibilites[i] * (i + 1) * (i + 1));
                    Eg += (double)(P * (i + 1));
                    Dg += (double)(P * (i + 1) * (i + 1));
                    Chi += (double)((events[i] * events[i]) / (N * possibilites[i]));
                    if(P > possibilites[i] + 0.1 || P < possibilites[i] - 0.1)
                    {
                        ans[i] = "Плохое приближение";
                    }
                    else
                    {
                        ans[i] = "Хорошее приближение";
                    }
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
                labelForEq1.Text = ans[0];
                labelForEq2.Text = ans[1];
                labelForEq3.Text = ans[2];
                labelForEq4.Text = ans[3];
                labelForEq5.Text = ans[4];
                labelForEq6.Text = ans[5];
            }
        }
    }
}
