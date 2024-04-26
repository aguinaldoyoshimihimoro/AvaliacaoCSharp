using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Net;
using System.Text;
using static System.Formats.Asn1.AsnWriter;
namespace System.Net;

public class Program
{
    public static void Main()
    {
        //Console.Write("Entre o ano do campeonato: ");
        //int year = int.Parse(Console.ReadLine());

        //Console.Write("Entre o nome do time: ");
        //string teamName = Console.ReadLine();

        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(year, teamName, "");

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(year, teamName, "");

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);
    }

    public static int getTotalScoredGoals(int year, string team, string team2)
    {
        long quantidadePaginas = GetPagesResults(year, team);
        int scoredGoals = 0;
        
        if (quantidadePaginas > 1)
        {
            for (int pagina = 1; pagina <= quantidadePaginas; pagina++)
            {
                scoredGoals = scoredGoals + GetTeamsResultsTeam1(year, team, pagina);
                scoredGoals = scoredGoals + GetTeamsResultsTeam2(year, team, pagina);
            }
        }
        else
        {
            scoredGoals = scoredGoals + GetTeamsResultsTeam1(year, team, 1);
            scoredGoals = scoredGoals + GetTeamsResultsTeam2(year, team, 1);
        }

        return scoredGoals;
    }

    public static long GetPagesResults(int ano, string time1)
    {
        string address = @"https://jsonmock.hackerrank.com/api/football_matches?year=YEARDATA&team1=TEAM1"
            .Replace("year=YEARDATA", "year=" + ano.ToString())
            .Replace("&team1=TEAM1", "&team1=" + time1.Trim());
        ;

        string retorno = "";
        int paginas = 0;

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(address);
        request.Method = "GET";
        request.ContentType = "application/json";

        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        using (Stream stream = response.GetResponseStream())
        using (StreamReader reader = new StreamReader(stream))
        {
            retorno = reader.ReadToEnd();
        }

        AllDatas allDatas = JsonConvert.DeserializeObject<AllDatas>(retorno);

        if (allDatas != null)
        {
            return allDatas.total_pages; 

        }
        
        return paginas;
    }




    public static int GetTeamsResultsTeam1(int ano, string time1, int pagina)
    {
        string address = @"https://jsonmock.hackerrank.com/api/football_matches?year=YEARDATA&team1=TEAM1&page=PAGE"
            .Replace("year=YEARDATA", "year=" + ano.ToString())
            .Replace("&team1=TEAM1", "&team1=" + time1.Trim())
            .Replace("&page=PAGE", "&page=" + pagina.ToString());
            ;

        string retorno = "";
        int scoredtime1 = 0;
        int scoredtime2 = 0;

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(address);
        request.Method = "GET";
        request.ContentType = "application/json";
                
        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        using (Stream stream = response.GetResponseStream())
        using (StreamReader reader = new StreamReader(stream))
        {
            retorno = reader.ReadToEnd();
        }

        AllDatas allDatas = JsonConvert.DeserializeObject<AllDatas>(retorno);


        if (allDatas != null)
        {
            foreach (var data in allDatas.data)
            {
                scoredtime1 = scoredtime1 + Convert.ToInt32(data.team1goals);
            }

        }
        else 
        {
            return 0;
        }
        return scoredtime1 - scoredtime2;
    }

    public static int GetTeamsResultsTeam2(int ano, string time2, int pagina)
    {
        string address = @"https://jsonmock.hackerrank.com/api/football_matches?year=YEARDATA&team2=TEAM2&page=PAGE"
            .Replace("year=YEARDATA", "year=" + ano.ToString())
            .Replace("&team2=TEAM2", "&team2=" + time2.Trim())
            .Replace("&page=PAGE", "&page=" + pagina.ToString());
        ;

        string retorno = "";
        int scoredtime1 = 0;
        int scoredtime2 = 0;

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(address);
        request.Method = "GET";
        request.ContentType = "application/json";

        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        using (Stream stream = response.GetResponseStream())
        using (StreamReader reader = new StreamReader(stream))
        {
            retorno = reader.ReadToEnd();
        }

        AllDatas allDatas = JsonConvert.DeserializeObject<AllDatas>(retorno);


        if (allDatas != null)
        {
            foreach (var data in allDatas.data)
            {
                scoredtime1 = scoredtime1 + Convert.ToInt32(data.team2goals);
            }

        }
        else
        {
            return 0;
        }
        return scoredtime1 - scoredtime2;
    }


    public class AllDatas
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public long total { get; set; }
        public long total_pages { get; set; }
        public List<Data> data { get; set; }
    }

    public class Data
    {
        public string? competition { get; set; }
        public int? year { get; set; }
        public string? round { get; set; }
        public string? team1 { get; set; }
        public string? team2 { get; set; }
        public string? team1goals { get; set; }
        public string? team2goals { get; set; }
    }
}