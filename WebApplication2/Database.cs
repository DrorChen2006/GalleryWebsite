using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebApplication2
{
    public class DatabaseAccess
    {
        private OleDbConnection GetConnection()
        {
            string location = HttpContext.Current.Server.MapPath("~/DataBase.MDB");
            OleDbConnection con = new OleDbConnection();
            con.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + location;
            con.Open();
            return con;
        }

        public List<Album> GetAllAlbums()
        {
            List<Album> albums = new List<Album>();
            Album currentAlbum = null;

            string query = "SELECT ALBUMS.ID, ALBUMS.ALBUM_NAME, ALBUMS.USER_ID, PICTURES.ID AS PICTURE_ID, PICTURES.PICTURE_NAME AS PICTURE_NAME, PICTURES.LOCATION " +
                   "FROM ALBUMS " +
                   "LEFT JOIN PICTURES ON ALBUMS.ID = PICTURES.ALBUM_ID " +
                   "ORDER BY ALBUMS.ID;";

            using (OleDbConnection con = GetConnection())
            {
                using (OleDbCommand command = con.CreateCommand())
                {
                    command.CommandText = query;
                    OleDbDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int albumId = Convert.ToInt32(reader["ID"]);
                        if (currentAlbum == null || currentAlbum.GetAlbumId() != albumId)
                        {
                            string albumName = reader["ALBUM_NAME"].ToString();
                            int userId = Convert.ToInt32(reader["USER_ID"]);

                            currentAlbum = new Album(userId, albumId, albumName, GetUsernameFromId(userId));
                            albums.Add(currentAlbum);
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("PICTURE_ID")))
                        {
                            int pictureId = Convert.ToInt32(reader["PICTURE_ID"]);
                            string pictureName = reader["PICTURE_NAME"].ToString();
                            string pictureLocation = reader["LOCATION"].ToString();

                            Picture picture = new Picture(pictureId, pictureName, pictureLocation);
                            currentAlbum.AddPicture(picture);
                        }
                    }
                }
            }

            return albums;
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            string query = "SELECT * FROM USERS;";

            using (OleDbConnection con = GetConnection())
            {
                using (OleDbCommand command = con.CreateCommand())
                {
                    command.CommandText = query;
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["ID"]);
                            string username = reader["USERNAME"].ToString();
                            string email = reader["EMAIL"].ToString();
                            string hashedPassword = reader["HASHED_PASSWORD"].ToString();

                            users.Add(new User(id, username, email, hashedPassword));
                        }
                    }
                }
            }

            return users;
        }

        public int SignupUser(string username, string email, string password)
        {
            try
            {
                if (!UserExists(username))
                {
                    string query = "INSERT INTO USERS (USERNAME, EMAIL, HASHED_PASSWORD) VALUES (@Username, @Email, @HashedPassword);";
                    using (OleDbConnection con = GetConnection())
                    {
                        using (OleDbCommand command = con.CreateCommand())
                        {
                            command.CommandText = query;
                            command.Parameters.AddWithValue("@Username", username);
                            command.Parameters.AddWithValue("@Email", email);
                            command.Parameters.AddWithValue("@HashedPassword", ToMd5Hash(password));
                            command.ExecuteNonQuery();
                        }
                    }
                    return LoginUser(username, password);
                }
                return -1;
            }
            catch { return -1; }
        }

        public int LoginUser(string username, string password)
        {
            try
            {
                string query = "SELECT * FROM USERS WHERE USERNAME = @Username;";
                using (OleDbConnection con = GetConnection())
                {
                    using (OleDbCommand command = con.CreateCommand())
                    {
                        command.CommandText = query;
                        command.Parameters.AddWithValue("@Username", username);
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string hashedPassword = reader["HASHED_PASSWORD"].ToString();
                                if (hashedPassword == ToMd5Hash(password))
                                {
                                    return Convert.ToInt32(reader["ID"]);
                                }
                            }
                        }
                    }
                }
            }
            catch { return -1; }
            return -1;
        }

        public bool UserExists(string username)
        {
            string query = "SELECT COUNT(*) FROM USERS WHERE USERNAME = @Username;";
            int count = 0;

            using (OleDbConnection con = GetConnection())
            {
                using (OleDbCommand command = con.CreateCommand())
                {
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@Username", username);
                    count = Convert.ToInt32(command.ExecuteScalar());
                }
            }

            return count != 0;
        }

        public string GetUsernameFromId(int userId)
        {
            string username = null;
            string query = "SELECT USERNAME FROM USERS WHERE ID = @UserId;";

            using (OleDbConnection con = GetConnection())
            {
                using (OleDbCommand command = con.CreateCommand())
                {
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@UserId", userId);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            username = reader["USERNAME"].ToString();
                        }
                    }
                }
            }

            return username;
        }

        public Album GetAlbumFromId(int albumId)
        {
            Album album = null;
            string query = "SELECT ALBUMS.ID, ALBUMS.ALBUM_NAME, ALBUMS.USER_ID, PICTURES.ID AS PICTURE_ID, PICTURES.PICTURE_NAME AS PICTURE_NAME, PICTURES.LOCATION " +
                           "FROM ALBUMS " +
                           "LEFT JOIN PICTURES ON ALBUMS.ID = PICTURES.ALBUM_ID " +
                           "WHERE ALBUMS.ID = @AlbumId;";

            using (OleDbConnection con = GetConnection())
            {
                using (OleDbCommand command = con.CreateCommand())
                {
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@AlbumId", albumId);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (album == null)
                            {
                                string albumName = reader["ALBUM_NAME"].ToString();
                                int userId = Convert.ToInt32(reader["USER_ID"]);
                                album = new Album(userId, albumId, albumName, GetUsernameFromId(userId));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("PICTURE_ID")))
                            {
                                int pictureId = Convert.ToInt32(reader["PICTURE_ID"]);
                                string pictureName = reader["PICTURE_NAME"].ToString();
                                string pictureLocation = reader["LOCATION"].ToString();

                                Picture picture = new Picture(pictureId, pictureName, pictureLocation);
                                album.AddPicture(picture);
                            }
                        }
                    }
                }
            }

            return album;
        }

        public Picture GetPictureFromId(int pictureId)
        {
            Picture picture = null;
            string query = "SELECT PICTURES.*, ALBUMS.USER_ID " +
                           "FROM PICTURES " +
                           "LEFT JOIN ALBUMS ON PICTURES.ALBUM_ID = ALBUMS.ID " +
                           "WHERE PICTURES.ID = @PictureId;";

            using (OleDbConnection con = GetConnection())
            {
                using (OleDbCommand command = con.CreateCommand())
                {
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@PictureId", pictureId);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string pictureName = reader["PICTURE_NAME"].ToString();
                            string location = reader["LOCATION"].ToString();
                            int userId = Convert.ToInt32(reader["USER_ID"]);

                            picture = new Picture(pictureId, pictureName, location, userId);
                        }
                    }
                }
            }

            return picture;
        }

        public bool CreatePicture(int albumId, string pictureName, string pictureLocation)
        {
            try
            {
                string query = "INSERT INTO PICTURES (PICTURE_NAME, LOCATION, ALBUM_ID) VALUES (@PictureName, @PictureLocation, @AlbumId);";
                using (OleDbConnection con = GetConnection())
                {
                    using (OleDbCommand command = con.CreateCommand())
                    {
                        command.CommandText = query;
                        command.Parameters.AddWithValue("@PictureName", pictureName);
                        command.Parameters.AddWithValue("@PictureLocation", pictureLocation);
                        command.Parameters.AddWithValue("@AlbumId", albumId);
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void CreateAlbum(int ownerId, string albumName)
        {
            try
            {
                string query = "INSERT INTO ALBUMS (ALBUM_NAME, USER_ID) VALUES (@AlbumName, @UserId);";
                using (OleDbConnection con = GetConnection())
                {
                    using (OleDbCommand command = con.CreateCommand())
                    {
                        command.CommandText = query;
                        command.Parameters.AddWithValue("@AlbumName", albumName);
                        command.Parameters.AddWithValue("@UserId", ownerId);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }

        public bool DeleteUser(int userId)
        {
            string query = "SELECT ID FROM ALBUMS WHERE USER_ID = @UserId;";
            List<int> albumIds = new List<int>();

            try
            {
                using (OleDbConnection con = GetConnection())
                {
                    using (OleDbCommand command = con.CreateCommand())
                    {
                        command.CommandText = query;
                        command.Parameters.AddWithValue("@UserId", userId);
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                albumIds.Add(reader.GetInt32(0));
                            }
                        }
                    }
                }
            }
            catch { return false; }

            // Delete each album of user
            foreach (int albumId in albumIds)
            {
                if (!DeleteAlbum(albumId))
                {
                    return false;
                }
            }

            // Delete the itself user
            string deleteUserQuery = "DELETE FROM USERS WHERE ID = @UserId;";
            try
            {
                using (OleDbConnection con = GetConnection())
                {
                    using (OleDbCommand command = con.CreateCommand())
                    {
                        command.CommandText = deleteUserQuery;
                        command.Parameters.AddWithValue("@UserId", userId);
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch { return false; }
        }
        
        public bool DeleteAlbum(int albumId)
        {
            try
            {
                string[] queries = {
                    // Delete pictures from album
                    "DELETE FROM PICTURES WHERE ALBUM_ID = @AlbumId;",
                    // Delete album itself
                    "DELETE FROM ALBUMS WHERE ID = @AlbumId;"
                };
                foreach (string query in queries) {
                    using (OleDbConnection con = GetConnection())
                    {
                        using (OleDbCommand command = con.CreateCommand())
                        {
                            command.CommandText = query;
                            command.Parameters.AddWithValue("@AlbumId", albumId);
                            command.ExecuteNonQuery();
                        }
                    }
                }
                return true;
            }
            catch { return false; }
        }

        public bool DeletePicture(int pictureId)
        {
            try
            {
                string query = "DELETE FROM PICTURES WHERE ID = @PictureId;";
                using (OleDbConnection con = GetConnection())
                {
                    using (OleDbCommand command = con.CreateCommand())
                    {
                        command.CommandText = query;
                        command.Parameters.AddWithValue("@PictureId", pictureId);
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateInfo(int id, string tableName, List<UpdateInfoParameter> updateParameters)
        {
            try
            {
                string query = "UPDATE " + tableName + " SET ";

                for (int i = 0; i < updateParameters.Count(); i++)
                {
                    if (updateParameters[i].ShouldUpdate())
                    {
                        query += updateParameters[i].GetFieldName() + $" = @Parm{i}, ";
                    }
                }

                if (!query.EndsWith(", "))
                {
                    return false;
                }

                query = query.Remove(query.Length - 2) + " WHERE ID = @id;";

                using (OleDbConnection con = GetConnection())
                {
                    using (OleDbCommand command = con.CreateCommand())
                    {
                        command.CommandText = query;
                        for (int i = 0; i < updateParameters.Count(); i++)
                        {
                            if (updateParameters[i].ShouldUpdate())
                            {
                                command.Parameters.AddWithValue($" = @Parm{i}", updateParameters[i].GetNewValue());
                            }
                        }

                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string ToMd5Hash(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}