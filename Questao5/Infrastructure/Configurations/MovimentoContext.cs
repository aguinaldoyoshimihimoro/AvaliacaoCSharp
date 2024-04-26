using Microsoft.Data.Sqlite;
using Questao5.Infrastructure.Services;
using Questao5.Infrastructure.Sqlite;
using System.Collections.Generic;
using System.Data.Entity;
using System.Reflection.Emit;
using Dapper;
using System.Linq;
using Questao5.Domain.Entities;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Humanizer;
using System.Globalization;

namespace Questao5.Infrastructure.Configurations
{
    public class MovimentoContext
    {
        private readonly DatabaseConfig databaseConfig;

        public MovimentoContext()
        {
            DatabaseConfig databaseConfig = new DatabaseConfig();
            this.databaseConfig = databaseConfig;
        }

        public MovimentoContext(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public List<movimentocontacorrente> InsertMovimento(int idContaCorrente, double valormovimento, string tipomovimento)
        {
            var MovimentoContaCorrentelist = new List<movimentocontacorrente>();

            if (valormovimento <= 0)
            {
                MovimentoContaCorrentelist.Add(new movimentocontacorrente()
                {
                    valormovimento = valormovimento,
                    numero = idContaCorrente,
                    tipomovimento = tipomovimento,
                    descricao = "INVALID_VALUE",
                    status = 0
                });

                return MovimentoContaCorrentelist;
            }

            if (tipomovimento.ToLower() != "c" && tipomovimento.ToLower() != "d")
            {
                MovimentoContaCorrentelist.Add(new movimentocontacorrente()
                {
                    valormovimento = valormovimento,
                    numero = idContaCorrente,
                    tipomovimento = tipomovimento,
                    descricao = "INVALID_TYPE",
                    status = 0
                });

                return MovimentoContaCorrentelist;
            }

            string Nome = "";
            int Ativo = 0;

            DateTime DataConsulta = new DateTime();

            DataConsulta = DateTime.Now;
            
            String strDataConsulta = DataConsulta.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");

            SqliteConnection connection = new SqliteConnection("Data Source=database.sqlite;");

            string sql = "SELECT idcontacorrente, nome, ativo FROM contacorrente WHERE numero= '" + idContaCorrente.ToString() + "'";
            SqliteCommand command = new SqliteCommand(sql, connection);
            connection.Open();

            SqliteDataReader reader = command.ExecuteReader();
            var ContaCorrentelist = new List<saldocontacorrenteTemp>();

            if (reader != null)
            {
                while (reader.Read())
                {
                    Nome = reader.GetString(1);
                    Ativo = reader.GetInt16(2);
                }
            }
            else
            {
                MovimentoContaCorrentelist.Add(new movimentocontacorrente()
                {
                valormovimento = valormovimento,
                        numero = idContaCorrente,
                        tipomovimento = tipomovimento,
                        descricao = "INVALID_ACCOUNT",
                        status = 0
                    });

                    connection.Close();
                    connection.Dispose();

                return MovimentoContaCorrentelist;
            }
    

            if (Ativo == 0)
            {
                MovimentoContaCorrentelist.Add(new movimentocontacorrente()
                {
                    valormovimento = valormovimento,
                    numero = idContaCorrente,
                    tipomovimento = tipomovimento,
                    descricao = "INACTIVE_ACCOUNT",
                    status = 0
                });

                connection.Close();
                connection.Dispose();

                return MovimentoContaCorrentelist;
            }
                        
            sql = "INSERT INTO movimento(idcontacorrente, datamovimento, tipomovimento, valor) VALUES('" + idContaCorrente + "','" + strDataConsulta + "', '" + tipomovimento.ToUpper() + "', '" + valormovimento.ToString() +  "'); ";
            command = new SqliteCommand(sql, connection);
            command.ExecuteNonQuery();

            MovimentoContaCorrentelist.Add(new movimentocontacorrente()
            {
                valormovimento = valormovimento,
                numero = idContaCorrente,
                tipomovimento = tipomovimento,
                datamovimento = DataConsulta,
                status = 0,
                descricao = "Transação Ok"
            });

            connection.Close();
            connection.Dispose();

            return MovimentoContaCorrentelist;
        }

    }
}




