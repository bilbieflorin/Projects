using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Utilities
{
    public class Photo
    {
        private string src;
        private string ownerName;
        private string desc;
        private DateTime up;
        private int id;

        public Photo(string source, string name,string descp, DateTime date, int imgId)
        {
            this.src = source;
            this.ownerName = name;
            this.desc = descp;
            this.up = date;
            this.id = imgId;
        }

        public string getSource() { return this.src; }

        public string getOwner() { return this.ownerName; }

        public string getDescription() { return this.desc; }

        public DateTime getDate() { return this.up; }

        public int getId() { return this.id; }
    }

    public class Comment
    {
        private string text, name;
        private DateTime date;
        private int user_id,comment_id; 

        public Comment(string txt, string user,int userId,int comId,DateTime d)
        {
            this.date = d;
            this.name = user;
            this.text = txt;
            this.user_id = userId;
            this.comment_id = comId;
        }

        public string getText() { return this.text; }

        public string getUserName() { return this.name; }

        public DateTime getDate() { return this.date; }

        public int getUserId() { return this.user_id; }

        public int getCommId() { return this.comment_id; } 
    }

    public class Album
    {
        private int id;
        private string desc;
        private string name;

        public Album(int i, string n, string d)
        {
            this.desc = d;
            this.id = i;
            this.name = n;
        }

        public int getId() { return this.id; }
        
        public string getDescription() { return this.desc; }

        public string getName() { return this.name; }
    }

    public class Functions
    {
        static public Hashtable getCategories(string con)
        {
            Hashtable ht = new Hashtable();
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(@"select category_id,category_name from categories", connect);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    int id;
                    string name;
                    while (reader.Read())
                    {
                        id = reader.GetInt32(0);
                        name = reader.GetString(1);
                        ht.Add(name, id);
                    }
                }
            }
            return ht;
        }

        static public Hashtable getAlbums(string con, int user_id)
        {
            Hashtable ht = new Hashtable();
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(@"select album_id,album_name from albums where owner_id = @id", connect);
                cmd.Parameters.Add(new SqlParameter("@id", user_id));
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    int id;
                    string name;
                    while (reader.Read())
                    {
                        id = reader.GetInt32(0);
                        name = reader.GetString(1);
                        ht.Add(name, id);
                    }
                }
            }
            return ht;
        }

        static public int numberOfAlbums(int userId, string conString)
        {
            using (SqlConnection connect = new SqlConnection(conString))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(@"select count(album_id) 
                                                  from albums
                                                  where OWNER_ID = @user", connect);
                cmd.Parameters.Add(new SqlParameter("@user", userId));
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    return reader.GetInt32(0);
                }
            }
        }

        static public int nextId(string table, string field, string conString)
        {
            using (SqlConnection connect = new SqlConnection(conString))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(@"select max(" + field + ") from " + table, connect);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    if (reader.IsDBNull(0))
                        return 1;
                    return reader.GetInt32(0) + 1;
                }
            }
        }

        static public List<Photo> getPhotosByUser(int userId, string conString)
        {
            List<Photo> l = new List<Photo>();
            using (SqlConnection connect = new SqlConnection(conString))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(@"select top 10 src, photo_id, upload_date, user_name, isnull(first_name,'')+' '+isnull(last_name,'') 
                                                  from photos join users on photos.owner_id = users.user_id
                                                  where photos.owner_id = @user 
                                                  order by photos.upload_date desc", connect);
                cmd.Parameters.Add(new SqlParameter("@user", userId));
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    string src, name;
                    DateTime d;
                    int id;
                    while (reader.Read())
                    {
                        src = reader.GetString(0);
                        id = reader.GetInt32(1);
                        d = reader.GetDateTime(2);
                        if (reader.GetString(4).Length == 1)
                            name = reader.GetString(3);
                        else
                            name = reader.GetString(4);
                        l.Add(new Photo(src, name, null, d, id));
                    }
                }
            }
            return l;
        }

        static public List<Photo> getPhotos(string conString)
        {
            List<Photo> l = new List<Photo>();
            using (SqlConnection connect = new SqlConnection(conString))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(@"select top 10 src, photo_id, upload_date, user_name, isnull(first_name,'')+' '+isnull(last_name,'') 
                                                  from photos join users on photos.owner_id = users.user_id 
                                                  order by photos.upload_date desc", connect);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    string src, name;
                    DateTime d;
                    int id;
                    while (reader.Read())
                    {
                        src = reader.GetString(0);
                        id = reader.GetInt32(1);
                        d = reader.GetDateTime(2);
                        if (reader.GetString(4).Length == 1)
                            name = reader.GetString(3);
                        else
                            name = reader.GetString(4);
                        l.Add(new Photo(src, name, null, d, id));
                    }
                }
            }
            return l;
        }

        static public List<Photo> getPhotosByAlbum(int albumId, string conString)
        {
            List<Photo> l = new List<Photo>();
            using (SqlConnection connect = new SqlConnection(conString))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(@"select top 10 src, photo_id, upload_date, user_name, isnull(first_name,'')+' '+isnull(last_name,'') 
                               from photos join users on photos.owner_id = users.user_id
                               where photos.album_id = @album 
                               order by photos.upload_date desc", connect);
                cmd.Parameters.Add(new SqlParameter("@album", albumId));
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    string src, name;
                    DateTime d;
                    int id;
                    while (reader.Read())
                    {
                        src = reader.GetString(0);
                        id = reader.GetInt32(1);
                        d = reader.GetDateTime(2);
                        if (reader.GetString(4).Length == 1)
                            name = reader.GetString(3);
                        else
                            name = reader.GetString(4);
                        l.Add(new Photo(src, name, null, d, id));
                    }
                }
            }
            return l;
        }

        static public Photo getPhoto(int id, string conString)
        {
            Photo p;
            using (SqlConnection connect = new SqlConnection(conString))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(@"select description, src, upload_date, user_name, isnull(first_name,'') +' '+ isnull(last_name,'')
                                                  from users join photos on photos.owner_id = users.user_id
                                                  where photo_id = @id", connect);
                cmd.Parameters.Add(new SqlParameter("@id", id));
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    string desc, src, name;
                    DateTime date;
                    reader.Read();
                    desc = reader.GetString(0);
                    src = reader.GetString(1);
                    date = reader.GetDateTime(2);
                    if (reader.GetString(4).Length == 1)
                        name = reader.GetString(3);
                    else
                        name = reader.GetString(4);
                    p = new Photo(src, name, desc, date, 0);
                }
            }
            return p;
        }

        static public List<Comment> getComments(int id, string conString)
        {
            List<Comment> l = new List<Comment>();
            using (SqlConnection connect = new SqlConnection(conString))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(@"select text, comment_date, user_name, isnull(first_name,'') +' '+ isnull(last_name,''),users.user_id,comment_id
                                                  from comments join users on comments.user_id = users.user_id
                                                  where photo_id = @id
                                                  order by comment_date asc", connect);
                cmd.Parameters.Add(new SqlParameter("@id", id));
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string name, text;
                        DateTime date;
                        int userId, comId;
                        text = reader.GetString(0);
                        if (reader.GetString(3).Length == 1)
                            name = reader.GetString(2);
                        else
                            name = reader.GetString(3);
                        date = reader.GetDateTime(1);
                        userId = reader.GetInt32(4);
                        comId = reader.GetInt32(5);
                        l.Add(new Comment(text, name, userId, comId, date));
                    }
                }
            }
            return l;
        }

        static public int getOwnerId(int id, string conString)
        {
            using (SqlConnection connect = new SqlConnection(conString))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(@"select owner_id 
                                                  from photos  
                                                  where photo_id = @id", connect);
                cmd.Parameters.Add(new SqlParameter("@id", id));
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    return reader.GetInt32(0);
                }
            }
        }

        public static void inserComment(string text, int userId, int photoId, string conString)
        {
            using (SqlConnection connect = new SqlConnection(conString))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(@"insert into comments values(@id,@text,@photo,@user,@date)", connect);
                int id = nextId("comments", "comment_id", conString);
                cmd.Parameters.Add(new SqlParameter("@id", id));
                cmd.Parameters.Add(new SqlParameter("@text", text));
                cmd.Parameters.Add(new SqlParameter("@photo", photoId));
                cmd.Parameters.Add(new SqlParameter("@user", userId));
                cmd.Parameters.Add(new SqlParameter("@date", DateTime.Now));
                cmd.ExecuteNonQuery();
            }
        }

        public static void deleteComment(int id, string conString)
        {
            using (SqlConnection connect = new SqlConnection(conString))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(@"delete from comments where comment_id = @id", connect);
                cmd.Parameters.Add(new SqlParameter("@id", id));
                cmd.ExecuteNonQuery();
            }
        }

        public static List<Album> getAlbums(int userId, string conString)
        {
            List<Album> l = new List<Album>();
            using (SqlConnection connect = new SqlConnection(conString))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(@"select album_id, album_name, description
                                                  from albums
                                                  where owner_id = @id", connect);
                cmd.Parameters.Add(new SqlParameter("@id", userId));
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string desc = reader.GetString(2);
                        l.Add(new Album(id, name, desc));
                    }
                }
            }
            return l;
        }

        public static Album getDetails(int albumId, string conString)
        {
            using (SqlConnection connect = new SqlConnection(conString))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(@"select album_name, description
                                                  from albums
                                                  where album_id = @id", connect);
                cmd.Parameters.Add(new SqlParameter("@id", albumId));
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    string name = reader.GetString(0);
                    string desc = reader.GetString(1);
                    return new Album(0, name, desc);

                }
            }
        }

        public static void updateAlbum(int albumId, string name, string desc, string conString)
        {
            using (SqlConnection connect = new SqlConnection(conString))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(@"update albums 
                                                  set album_name = @name, description = @desc
                                                  where album_id = @id", connect);
                cmd.Parameters.Add(new SqlParameter("@id", albumId));
                cmd.Parameters.Add(new SqlParameter("@name", name));
                cmd.Parameters.Add(new SqlParameter("@desc", desc));
                cmd.ExecuteNonQuery();
            }
        }

        public static string getUserName(int userId, string conString)
        {
            using (SqlConnection connect = new SqlConnection(conString))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(@"select user_name
                                                  from users
                                                  where user_id = @id", connect);
                cmd.Parameters.Add(new SqlParameter("@id", userId));
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    return reader.GetString(0);
                }
            }
        }

        public static string getFirstName(int userId, string conString)
        {
            using (SqlConnection connect = new SqlConnection(conString))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(@"select first_name
                                                  from users
                                                  where user_id = @id", connect);
                cmd.Parameters.Add(new SqlParameter("@id", userId));
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    return (reader.IsDBNull(0) ? "" : reader.GetString(0));
                }
            }
        }

        public static string getLastName(int userId, string conString)
        {
            using (SqlConnection connect = new SqlConnection(conString))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(@"select last_name
                                                  from users
                                                  where user_id = @id", connect);
                cmd.Parameters.Add(new SqlParameter("@id", userId));
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    return (reader.IsDBNull(0) ? "" : reader.GetString(0));
                }
            }
        }

        public static string getEmail(int userId, string conString)
        {
            using (SqlConnection connect = new SqlConnection(conString))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(@"select email
                                                  from users
                                                  where user_id = @id", connect);
                cmd.Parameters.Add(new SqlParameter("@id", userId));
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    return reader.GetString(0);
                }
            }
        }

        public static DateTime getBirthday(int userId, string conString)
        {
            using (SqlConnection connect = new SqlConnection(conString))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(@"select birthday
                                                  from users
                                                  where user_id = @id", connect);
                cmd.Parameters.Add(new SqlParameter("@id", userId));
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    return (reader.IsDBNull(0) ? default(DateTime) : reader.GetDateTime(0));
                }
            }
        }

        public static string getGender(int userId, string conString)
        {
            using (SqlConnection connect = new SqlConnection(conString))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(@"select gender
                                                  from users
                                                  where user_id = @id", connect);
                cmd.Parameters.Add(new SqlParameter("@id", userId));
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    return reader.GetString(0);
                }
            }
        }

        public static string getPassword(int userId, string conString)
        {
            using (SqlConnection connect = new SqlConnection(conString))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(@"select password
                                                  from users
                                                  where user_id = @id", connect);
                cmd.Parameters.Add(new SqlParameter("@id", userId));
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    return reader.GetString(0);
                }
            }
        }

        public static void updateAccountprotected(int id, string userName, string password, string email, string gender, string fName, string lName, DateTime bDay, string conString)
        {
            using (SqlConnection connect = new SqlConnection(conString))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(@"update users
                                                  set USER_NAME = @user,
                                                      PASSWORD = @pass, 
                                                      EMAIL = @email, 
                                                      GENDER = @gender,
                                                      FIRST_NAME = @first_name, 
                                                      LAST_NAME = @last_name,
                                                      BIRTHDAY = @birthday
                                                  where USER_ID = @id", connect);
                cmd.Parameters.Add(new SqlParameter("@id", id));
                cmd.Parameters.Add(new SqlParameter("@user", userName.ToLower()));
                cmd.Parameters.Add(new SqlParameter("@pass", password));
                cmd.Parameters.Add(new SqlParameter("@email", email));
                cmd.Parameters.Add(new SqlParameter("@gender", gender));
                if (fName != null)
                    cmd.Parameters.Add(new SqlParameter("@first_name", fName));
                else
                    cmd.Parameters.Add(new SqlParameter("@first_name", (object)DBNull.Value));
                if (lName != null)
                    cmd.Parameters.Add(new SqlParameter("@last_name", lName));
                else
                    cmd.Parameters.Add(new SqlParameter("@last_name", (object)DBNull.Value));
                if (bDay == default(DateTime))
                    cmd.Parameters.Add(new SqlParameter("@birthday", (object)DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@birthday", bDay));
                cmd.ExecuteNonQuery();
            }
        }

        public static bool findInDatabase(string columnName, string value, string conString)
        {
            using (SqlConnection connect = new SqlConnection(conString))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(@"select user_id 
                                          from users 
                                          where " + columnName + " = @value", connect);
                cmd.Parameters.Add(new SqlParameter("@value", value.ToLower()));
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return false;
                    return true;
                }
            }
        }

        public static List<Photo> search(string query, int category, string conString)
        {
            List<Photo> l = new List<Photo>();
            using (SqlConnection connect = new SqlConnection(conString))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(@"select src, photo_id, upload_date, user_name, isnull(first_name,'')+' '+isnull(last_name,'') 
                                                  from photos join users on photos.owner_id = users.user_id 
                                                  where description like '%'+@query+'%' and category_id = @category", connect);
                cmd.Parameters.Add(new SqlParameter("@query", query));
                cmd.Parameters.Add(new SqlParameter("@category", category));
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    string src, name;
                    DateTime d;
                    int id;
                    while (reader.Read())
                    {
                        src = reader.GetString(0);
                        id = reader.GetInt32(1);
                        d = reader.GetDateTime(2);
                        if (reader.GetString(4).Length == 1)
                            name = reader.GetString(3);
                        else
                            name = reader.GetString(4);
                        l.Add(new Photo(src, name, null, d, id));
                    }
                }
            }
            return l;
        }

        public static List<Photo> search(string query, string conString)
        {
            List<Photo> l = new List<Photo>();
            using (SqlConnection connect = new SqlConnection(conString))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(@"select src, photo_id, upload_date, user_name, isnull(first_name,'')+' '+isnull(last_name,'') 
                                                  from photos join users on photos.owner_id = users.user_id 
                                                  where description like '%'+@query+'%'", connect);
                cmd.Parameters.Add(new SqlParameter("@query",query));
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    string src, name;
                    DateTime d;
                    int id;
                    while (reader.Read())
                    {
                        src = reader.GetString(0);
                        id = reader.GetInt32(1);
                        d = reader.GetDateTime(2);
                        if (reader.GetString(4).Length == 1)
                            name = reader.GetString(3);
                        else
                            name = reader.GetString(4);
                        l.Add(new Photo(src, name, null, d, id));
                    }
                }
            }
            return l;
        }

        public static void deletePhoto(int id, string conString)
        {
            using(SqlConnection connect = new SqlConnection(conString))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(@"delete from photos where photo_id = @id",connect);
                cmd.Parameters.Add(new SqlParameter("@id",id));
                cmd.ExecuteNonQuery();
            }
        }
    }
}