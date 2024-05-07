using System;
using System.Collections.Generic;

namespace WebApplication2
{
    public class Picture
    {
        private int m_pictureId;
        private string m_name;
        private string m_location;
        private int m_ownerId;

        public Picture(int id, string name, string location)
        {
            m_pictureId = id;
            m_name = name;
            m_location = location;
        }

        public Picture(int id, string name, string location, int ownerId)
        {
            m_pictureId = id;
            m_name = name;
            m_location = location;
            m_ownerId = ownerId;
        }

        public int GetId()
        {
            return m_pictureId;
        }

        public string GetName()
        {
            return m_name;
        }

        public string GetLocation()
        {
            return m_location;
        }

        public int GetOwnerId()
        {
            return m_ownerId;
        }
    }
}
