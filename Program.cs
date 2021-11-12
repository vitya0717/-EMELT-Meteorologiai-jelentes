using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace metjelentes
{
    class tavirat
    {
        public string telepules;
        public string ido;
        public string szelirany;
        public int homersek;

        public tavirat(string telepules, string ido, string szelirany, int homersek)
        {
            this.telepules = telepules;
            this.ido = ido;
            this.szelirany = szelirany;
            this.homersek = homersek;
        }
        
        public int getOra()
        {
            return int.Parse(ido.Substring(0,2));
        }
        public int getPerc()
        {
            return int.Parse(ido.Substring(2, 2));
        }

        public string getSzelirany()
        {
            return szelirany.Substring(0,3);
        }
        public string getSzelerosseg()
        {
            return szelirany.Substring(2, 2);
        }

        public string getFormazottIdo(string ido)
        {
           
            return ido.Substring(0, 2)+":"+ido.Substring(2, 2);
        }

    }

    class Program
    {
        static void Main(string[] args)
        {

            string[] sorok = File.ReadAllLines("tavirathu13.txt");
            List<tavirat> taviratok = new List<tavirat>();

            foreach (var item in sorok)
            {
                string[] darabok = item.Split(' ');
                taviratok.Add(new tavirat( darabok[0], darabok[1], darabok[2], int.Parse(darabok[3])));
               
            }

            Console.WriteLine("2. feladat");
            Console.Write("Adja meg egy település kódját! Település: ");
            string kod = Console.ReadLine();

            tavirat max = taviratok[0];
            int index = 0;
            for (int i = 0; i < taviratok.Count; i++)
            {
                if(taviratok[i].getOra() > max.getOra() && taviratok[i].telepules == kod)
                {
                    max = taviratok[i];
                    index = i;

                } else if(taviratok[i].getOra() == max.getOra() && taviratok[i].getPerc() > max.getPerc() && taviratok[i].telepules == kod) {

                    max = taviratok[i];
                    index = i;
                }
            }
            Console.WriteLine("Az utolsó mérési adat a megadott településről {0}-kor érkezett.", taviratok[index].getFormazottIdo(taviratok[index].ido));


            Console.WriteLine("3. feladat ");
            tavirat homer = taviratok[0];
            
            for (int i = 0; i < taviratok.Count; i++)
            {
                if (taviratok[i].homersek < homer.homersek)
                {
                    homer = taviratok[i];
                    index = i;

                }
            }
            Console.WriteLine("A legalacsonyabb hőmérséklet: {0} {1} {2} Fok", homer.telepules,homer.getFormazottIdo(taviratok[index].ido), homer.homersek);

            for (int i = 0; i < taviratok.Count; i++)
            {
                if (taviratok[i].homersek > homer.homersek)
                {
                    homer = taviratok[i];
                    index = i;

                }
            }
            Console.WriteLine("A legmagasabb hőmérséklet: {0} {1} {2} Fok", homer.telepules, homer.getFormazottIdo(taviratok[index].ido), homer.homersek);


            /*Határozza meg, azokat a településeket és időpontokat, ahol és amikor a mérések idején 
            szélcsend volt! (A szélcsendet a táviratban 00000 kóddal jelölik.) Ha nem volt ilyen, akkor 
            a „Nem volt szélcsend a mérések idején.” szöveget írja ki! A kiírásnál a település kódját és 
            az időpontot jelenítse meg.*/

            Console.WriteLine("4. feladat");

            for (int i = 0; i < taviratok.Count; i++)
            {
                if(taviratok[i].getSzelerosseg() == "00" && taviratok[i].getSzelirany() == "000")
                {
                    Console.WriteLine("{0} {1}",taviratok[i].telepules, taviratok[i].getFormazottIdo(taviratok[i].ido));
                }
            }

            /*Határozza meg a települések napi középhőmérsékleti adatát és a hőmérséklet-ingadozását! 
            A kiírásnál a település kódja szerepeljen a sor elején a minta szerint! A kiírásnál csak 
            a megoldott feladatrészre vonatkozó szöveget és értékeket írja ki!

            BP Középhőmérséklet: 23; Hőmérséklet-ingadozás: 8
             */
            Console.WriteLine("5. feladat");
            List<string> telepulesek = new List<string>();
            List<int> orak = new List<int>();
            List<int> homersekletek = new List<int>();

            for (int i = 0; i < taviratok.Count; i++)
            {
                if (!telepulesek.Contains(taviratok[i].telepules))
                {
                    telepulesek.Add(taviratok[i].telepules);
                }
            }

            for (int i = 0; i < telepulesek.Count; i++)
            {
                string telepul = telepulesek[i];
                double atlag = 0;
                int db = 0;
                int inga = 0;

                for (int j = 0; j < taviratok.Count; j++)
                {
                    if(telepul == taviratok[j].telepules) 
                    homersekletek.Add(taviratok[j].homersek);

                    if ((telepul == taviratok[j].telepules) && (taviratok[j].getOra() == 01 || taviratok[j].getOra() == 07 || taviratok[j].getOra() == 13 || taviratok[j].getOra() == 19))
                    {
                        if(!orak.Contains(taviratok[j].getOra()))
                        {
                            orak.Add(taviratok[j].getOra());
                        }
                        atlag += taviratok[j].homersek;
                        db++;
                    }
                }
                homersekletek.Sort();
                inga = homersekletek[homersekletek.Count-1] - homersekletek[0];
                atlag = atlag / db;
                if(orak.Count == 4)
                {
                    Console.WriteLine("{0} Középhőmérséklet: {1}; Hőmérséklet-ingadozás: {2}", telepul, Math.Round(atlag, 0), inga);
                } else
                {
                    Console.WriteLine("{0} NA; Hőmérséklet-ingadozás: {1}", telepul, inga);
                }
                orak.Clear();
                homersekletek.Clear();
                /*atlag = 0;
                db = 0;
                inga = 0;*/
                

            }




            Console.ReadKey();

        }
    }
}
