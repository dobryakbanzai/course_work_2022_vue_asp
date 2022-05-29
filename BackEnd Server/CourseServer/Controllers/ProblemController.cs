using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using CourseServer.Models;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using Microsoft.AspNetCore.Cors;

namespace CourseServer.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProblemController : ControllerBase
    {
        [HttpGet]
        public async IAsyncEnumerable<Probs>/*List<Probs>*/ Get(string a , string b)
        {
            Generator generator = new Generator(10);
            Parcer parcer = new Parcer();
            string resultg = " ";
            while (resultg.Length < 3)
            {
                resultg = generator.Gen("E");
            }
            int res, i;
            (res, i) = parcer.Expr(resultg);
            string resulte = res.ToString();
            Probs prim1 = new Probs(resultg, resulte);
            List <Probs> pl = new List<Probs>();
            pl.Add(prim1);
            await Task.Delay(1000);
            yield return prim1;
            
        }
        


    }

    public class Prim
    {
        public string problem { set; get; }
        public string resolve { set; get; }
        public Prim(string problem, string resolve)
        {
            problem = problem;
            resolve = resolve;
        }
    }
    
    public class Generator
    {
        int lenght { get; set; }
        public Generator(int lenght) {
            this.lenght = lenght;
        }


        public string E(int len)
        {
            var rand = new Random();
            if (len <= this.lenght && len > 0) {
                var l = rand.Next(1, 4);
                if (l == 1)
                {
                    return "T1+E";
                }else if(l == 2)
                {
                    return "T1-E";
                }
                else if(l == 3)
                {
                    return "T1";
                }
            }
            else if(len > this.lenght){
                return "T1";
            }
            else if(len == -1)
            {
                var l = rand.Next(1, 3);
                if (l == 1)
                {
                    return "T1+E";
                }
                else if (l == 2)
                {
                    return "T1-E";
                }

            }
           
            return "T1";
        }

        public string T1(int len)
        {
            var rand = new Random();
            if (len < this.lenght)
            {
                var l = rand.Next(1, 4);
                if (l == 1)
                {
                    return "T2*T1";
                }
                else if (l == 2)
                {
                    return "T2";
                }
                else if (l == 3)
                {
                    return "T2";
                }

            }
            return "T2";
        }

        public string T2()
        {
            return "T3";
        }

        public string T3(int len)
        {
            var rand = new Random();
            if (len < this.lenght) {
                var l = rand.Next(1, 3);
                if (l == 1)
                {
                    return Nums();
                }
                else if (l == 2)
                {
                    var et3 = E(-1);
                    var st3 = "(" + et3 + ")";
                    return st3;
                }
            }
            return Nums();
        }

        public string Nums()
        {
            var rand = new Random();
            return rand.Next(1, 10).ToString();
        }

        public string Gen(string st = "E")
        {
            while (st.Contains("E") || st.Contains("T1") || st.Contains("T2") || st.Contains("T3"))
            {
                if(st.Contains("E"))
                {
                    int i = st.IndexOf("E");
                    st = st.Remove(i, "E".Length).Insert(i, E(st.Length));
                }
                if (st.Contains("T1"))
                {
                    int i = st.IndexOf("T1");
                    st = st.Remove(i, "T1".Length).Insert(i, T1(st.Length));
                }
                if (st.Contains("T2"))
                {
                    int i = st.IndexOf("T2");
                    st = st.Remove(i, "T2".Length).Insert(i, T2());
                }
                if (st.Contains("T3"))
                {
                    int i = st.IndexOf("T3");
                    st = st.Remove(i, "T3".Length).Insert(i, T3(st.Length));
                }
            }
            return st;
        }
    }

    public class Parcer
    {
        public (int a, int i) Num(string s, int i)
        {
            string st = "";
            int p = 1;
            while (i < s.Length && s[i] >= '0' && s[i] <= '9')
            {
                st += s[i];
                i += 1;
            }
            return (p * Convert.ToInt32(st), i);
        }

        public (int a, int i) Mult(string s, int i)
        {
            int a = 0;
            if (s[i].Equals('('))
            {
                i += 1;
                (a, i) = Expr(s, i);
                i += 1;
            }
            else
            {
                (a, i) = Num(s, i);
            }
            return (a, i);
        }

        public (int a, int i) Sum(string s, int i)
        {
            int a;
            (a, i) = Mult(s, i);
            while (i < s.Length && (s[i].Equals('*') || s[i].Equals('/')))
            {
                char ch = s[i];
                i += 1;
                int b;
                (b, i) = Mult(s, i);
                if (ch.Equals('*'))
                {
                    a *= b;
                }
                else
                {
                    a = a / b;
                }
            }
            return (a, i);
        }

        public (int a, int i) Expr(string s, int i = 0)
        {
            int a;
            (a, i) = Sum(s, i);
            while (i < s.Length && (s[i].Equals('+') || s[i].Equals('-')))
            {
                char ch = s[i];
                i += 1;
                int b;
                (b, i) = Sum(s, i);
                if (ch.Equals('+'))
                {
                    a += b;
                }
                else
                {
                    a -= b;
                }
            }
            return (a, i);
        }
    }
}