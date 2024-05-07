using System;
using System.Collections.Generic;


namespace WebApplication2
{
    public class Album
    {
        private int m_ownerId;
        private int m_albumId;
        private string m_name;
        private string m_ownerName;
        private List<Picture> m_pictures;

        public Album()
        {
            m_ownerId = 0;
            m_albumId = 0;
            m_name = "";
            m_ownerName = "";
            m_pictures = new List<Picture>();
        }

        public Album(int ownerId, int albumId, string name, string ownerName)
        {
            m_ownerId = ownerId;
            m_albumId = albumId;
            m_name = name;
            m_ownerName = ownerName;
            m_pictures = new List<Picture>();
        }

        public string GetName()
        {
            return m_name;
        }

        public int GetOwnerId()
        {
            return m_ownerId;
        }

        public int GetAlbumId()
        {
            return m_albumId;
        }

        public List<Picture> GetPictures()
        {
            return new List<Picture>(m_pictures);
        }

        public void AddPicture(Picture picture)
        {
            m_pictures.Add(picture);
        }
    }
}
