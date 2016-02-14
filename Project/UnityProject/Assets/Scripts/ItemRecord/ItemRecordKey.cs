﻿

public class ItemRecordKey
{
    private int m_x = 0;
    private int m_y = 1;

    public string m_code;

    public ItemRecordKey(int key1, int key2)
    {
        m_x = key1;
        m_y = key2;
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }

        if (typeof(ItemRecordKey).IsAssignableFrom(obj.GetType()))
        {
            ItemRecordKey twoArgKey = (ItemRecordKey)obj;
            if (twoArgKey.m_x == m_x && twoArgKey.m_y == m_y)
            {
                return true;
            }
        }

        return base.Equals(obj);
    }
    public override int GetHashCode()
    {
        return m_x;
    }
}