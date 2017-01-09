﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowArena.Models;

namespace DalFun2Application
{
    class PlayerSQLContext : IPlayerContext
    {
        private DatabaseManager DatabaseManager { get; set; }
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public PlayerSQLContext(DatabaseManager dbManager)
        {
            DatabaseManager = dbManager;
        }

        public void add(Player player)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO Player (userName, passWord, level, experience)" +
                              "VALUES(@username, @password, @level, @experience)";
            cmd.Parameters.AddWithValue("@username", player.UserName);
            cmd.Parameters.AddWithValue("@password", player.PassWord);
            cmd.Parameters.AddWithValue("@level", player.Level);
            cmd.Parameters.AddWithValue("@experience", player.Experience);
            DatabaseManager.runCommandNonQuery(cmd);
        }

        public void delete(Player player)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "DELETE FROM Player" +
                               " WHERE Player.id = @id";
            cmd.Parameters.AddWithValue("@id", player.Id);
            DatabaseManager.runCommandNonQuery(cmd);
        }

        public void update(Player player)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE Player SET userName=@username, passWord=@password, level=@level, experience=@experience" +
                               " WHERE Player.id = @id";
            cmd.Parameters.AddWithValue("@id", player.Id);
            cmd.Parameters.AddWithValue("@username", player.UserName);
            cmd.Parameters.AddWithValue("@password", player.PassWord);
            cmd.Parameters.AddWithValue("@level", player.Level);
            cmd.Parameters.AddWithValue("@experience", player.Experience);
            DatabaseManager.runCommandNonQuery(cmd);
        }

        public ICollection<Player> read()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Player";
            DbDataReader reader = DatabaseManager.runCommand(cmd);
            ICollection<Player> playerList = new List<Player>();
            while (reader.Read())
            {
                Player newPlayerModel = new Player();
                newPlayerModel.Id = reader.GetInt32(0);
                newPlayerModel.UserName = reader.GetString(1);
                newPlayerModel.PassWord = reader.GetString(2);
                newPlayerModel.Level = reader.GetInt32(3);
                newPlayerModel.Experience = reader.GetInt32(4);
                playerList.Add(newPlayerModel);
            }
            return playerList;
        }
    }
}