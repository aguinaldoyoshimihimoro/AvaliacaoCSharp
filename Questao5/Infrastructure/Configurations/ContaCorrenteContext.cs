using Microsoft.Data.Sqlite;
using Questao5.Infrastructure.Services;
using Questao5.Infrastructure.Sqlite;
using System.Collections.Generic;
using System.Data.Entity;
using System.Reflection.Emit;
using Dapper;
using System.Linq;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Configurations
{
    public class ContaCorrenteContext 
    {
        private readonly DatabaseConfig databaseConfig;

        public ContaCorrenteContext()
        {
            DatabaseConfig databaseConfig = new DatabaseConfig();
            this.databaseConfig = databaseConfig;
        }

        public ContaCorrenteContext(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public List<contacorrente> GetContaCorrente(int idContaCorrente)
        {
            SqliteConnection connection = new SqliteConnection("Data Source=database.sqlite;");
            
            string sql = "SELECT idcontacorrente, numero, nome, ativo FROM contacorrente WHERE numero= '"+ idContaCorrente.ToString() + "'";
            SqliteCommand command = new SqliteCommand(sql, connection);
            connection.Open();

            SqliteDataReader reader = command.ExecuteReader();
            var ContaCorrentelist = new List<contacorrente>();
            while (reader.Read())
            {
                ContaCorrentelist.Add(new contacorrente()
                {
                    idcontacorrente = reader.GetString(0),
                    numero = reader.GetInt32(1),
                    nome = reader.GetString(2),
                    ativo = reader.GetInt16(1)
                });
            }
            connection.Close();
            connection.Dispose();

            return ContaCorrentelist; 
        }

        public List<saldocontacorrenteTemp> GetSaldoContaCorrente(int idContaCorrente)
        {
            double saldocredito = 0;
            double saldodebito = 0;
            string Nome = "";
            int Ativo = 0;

            DateTime DataConsulta = new DateTime();

            DataConsulta = DateTime.Now;

            var SaldoContaCorrentelist = new List<saldocontacorrenteTemp>();
            SqliteConnection connection = new SqliteConnection("Data Source=database.sqlite;");

            string sql = "SELECT nome, ativo FROM contacorrente WHERE numero= '" + idContaCorrente.ToString() + "'";
            SqliteCommand command = new SqliteCommand(sql, connection);
            connection.Open();

            SqliteDataReader reader = command.ExecuteReader();
            var ContaCorrentelist = new List<saldocontacorrenteTemp>();
            while (reader.Read())
            {
                Nome = reader.GetString(0);
                Ativo = reader.GetInt16(1);
            }

            if (Ativo == 0)
            {
                SaldoContaCorrentelist.Add(new saldocontacorrenteTemp()
                {
                    saldo = saldocredito - saldodebito,
                    numero = idContaCorrente,
                    nome = Nome,
                    dataconsulta = DataConsulta,
                    status = 0
                });

                connection.Close();
                connection.Dispose();

                return SaldoContaCorrentelist;
            }

            sql = "SELECT ifnull(sum(valor),0) as saldo FROM movimento INNER JOIN contacorrente ON contacorrente.numero = movimento.idcontacorrente WHERE movimento.idcontacorrente= '" + idContaCorrente.ToString() + "' and movimento.tipomovimento= 'C' and contacorrente.ativo = 1";
            command = new SqliteCommand(sql, connection);
            reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                saldocredito = reader.GetInt64(0);
            }

            sql = "SELECT ifnull(sum(valor),0) as saldo FROM movimento INNER JOIN contacorrente ON contacorrente.numero = movimento.idcontacorrente WHERE movimento.idcontacorrente= '" + idContaCorrente.ToString() + "' and movimento.tipomovimento= 'D' and contacorrente.ativo = 1";
            command = new SqliteCommand(sql, connection);
            reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                saldodebito = reader.GetInt64(0);
            }

            SaldoContaCorrentelist.Add(new saldocontacorrenteTemp()
            {
                saldo = saldocredito - saldodebito,
                numero = idContaCorrente,
                nome = Nome,
                dataconsulta = DataConsulta,
                status = Ativo
            });

            connection.Close();
            connection.Dispose();

            return SaldoContaCorrentelist;
        }

    }
}




