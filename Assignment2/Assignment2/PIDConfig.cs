using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment2
{
    class PIDConfig : IComparable 
    {

        private static double alpha = 0.7;

        private MLApp.MLAppClass matlab;

        public static double MAX_kp = 17.99;
        public static double MIN_kp = 2.01;

        public static double MAX_ti = 9.41;
        public static double MIN_ti = 1.06;

        public static double MAX_td = 2.36;
        public static double MIN_td = 0.27;

        // 0 - Kp, 1 - Ti, 2 - Td
        private double[] param = new double[3];
        private double ise = -1;
        private double tr = -1;
        private double ts = -1;
        private double mp = -1;


        private double fitness = -1;

        public PIDConfig()
        {
            param[0] = MIN_kp;
            param[1] = MIN_ti;
            param[2] = MIN_td;
        }

        public PIDConfig( PIDConfig parent1, PIDConfig parent2) 
        {
            this.KP = Math.Round(parent1.KP * alpha + parent2.KP * (1 - alpha), 2);
            this.TI = Math.Round(parent1.TI * alpha + parent2.TI * (1 - alpha), 2);
            this.TD = Math.Round(parent1.TD * alpha + parent2.TD * (1 - alpha), 2);
        }

        public PIDConfig(double kp, double ti, double td)
        {
            param[0] = kp;
            param[1] = ti;
            param[2] = td;
        }

        public double KP
        {
            set { this.param[0] = value; }
            get { return this.param[0]; }
        }

        public double TI
        {
            set { this.param[1] = value; }
            get { return this.param[1]; }
        }

        public double TD
        {
            set { this.param[2] = value; }
            get { return this.param[2]; }
        }

        public double MP
        {
            get { return this.mp; }
        }

        public double TR
        {
            get { return this.tr; }
        }

        public double TS
        {
            get { return this.ts; }
        }

        public double ISE
        {
            get { return this.ise; }
        }

        public double getFitness()
        {
            //return some fitness level;
            if (this.fitness == -1)
            {
                matlab = new MLApp.MLAppClass();
                //matlab.Execute("cmd");

                StringBuilder sb = new StringBuilder();
                sb.Append("Kp = " + this.KP + ";");
                sb.Append("Ti = " + this.TI + ";");
                sb.Append("Td = " + this.TD + ";");
                sb.Append("G = Kp*tf([Ti*Td,Ti,1],[Ti,0]);");
                sb.Append("F = tf(1,[1,6,11,6,0]);");
                sb.Append("sys = feedback(series(G,F),1);");
                sb.Append("sysinf = stepinfo(sys);");
                sb.Append("t = 0:0.01:100;");
                sb.Append("y = step(sys,t);");
                sb.Append("ISE = sum((y-1).^2);");
                sb.Append("t_r = sysinf.RiseTime;");
                sb.Append("t_s = sysinf.SettlingTime;");
                sb.Append("M_p = sysinf.Overshoot;");

                matlab.Execute(sb.ToString());
                try
                {
                    this.ise = (double)matlab.GetVariable("ISE", "base");
                    //this.ise = Math.Round(this.ise, 2);
                }
                catch (InvalidCastException ice)
                {
                    this.ise = 9999.99; //Infinity
                }

                try
                {
                    this.tr = (double)matlab.GetVariable("t_r", "base");
                    //this.tr = Math.Round(this.tr, 2);
                }
                catch (InvalidCastException ice)
                {
                    this.tr = 9999.99; //Infinity
                }

                try
                {     
                    this.ts = (double)matlab.GetVariable("t_s", "base");
                    //this.ts = Math.Round(this.ts, 2);
                }
                catch (InvalidCastException ice)
                {
                    this.ts = 9999.99; //Infinity
                }

                try
                {
                    this.mp = (double)matlab.GetVariable("M_p", "base");
                    //this.mp = Math.Round(this.mp, 2);
                }
                catch (InvalidCastException ice)
                {
                    this.mp = 9999.99; //Infinity
                }

                this.fitness = 1/(this.ise + this.tr + this.ts  + this.mp );
            }
            return this.fitness;
        }

        public int CompareTo(Object obj)
        {
            if (obj is PIDConfig)
            {
                var pidConfig = (PIDConfig)obj;
                //return this.getFitness().CompareTo(pidConfig.getFitness());
                return pidConfig.getFitness().CompareTo(this.getFitness());
            }
            else
            {
                throw new ArgumentException("Object is not of type PID Config");
            }
        }

        public void Mutate()
        {
            Random r = new Random();
            double n = r.NextDouble() * 2 - 1;
            
            this.KP += n * Math.Min(this.KP - MIN_kp, MAX_kp - this.KP);

            this.TD += n * Math.Min(this.TD - MIN_td, MAX_td - this.TD);

            this.TI += n * Math.Min(this.TI - MIN_ti, MAX_ti - this.TI);

            this.KP = Math.Round(Math.Max(Math.Min(MAX_kp,this.KP),MIN_kp),2);
            this.TD = Math.Round(Math.Max(Math.Min(MAX_td, this.TD), MIN_td),2);
            this.TI = Math.Round(Math.Max(Math.Min(MAX_ti, this.TI), MIN_ti),2);

            this.fitness = -1;
        }

        public String ToString()
        {
            return "PIDConfig: Kp: " + this.KP + " Td: " + this.TD + " ti: " + this.TI + " ISE: " + this.ise + " tr: " + this.tr + " ts: " + this.ts + " Mp: " + this.mp + " Fitness: " + this.fitness;
            //return  this.KP + "|" + this.TD + "|" + this.TI + "|" + this.ise + "|" + this.tr + "|" + this.ts + "|" + this.mp + "|" + this.fitness;
        }
    }
}
