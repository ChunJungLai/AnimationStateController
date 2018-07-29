using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BadBird.StringStrategy;

namespace BadBird
{

using Database = Dictionary<string, StringCollection>;

public class StringDatabase
{
    private Database database = new Database();

    public void AddCollection(string key, Parameter parameter)
    {
        database.Add(key, new StringCollection(parameter));
    }

    public void RemoveCollection(string key)
    {
        database.Remove(key);
    }

    public bool HasCollection(string key)
    {
        return database.ContainsKey(key);
    }

    public StringCollection GetCollection(string key)
    {
        StringCollection c = null;
        try
        {
            c = database[key];
        }
        catch
        {
            Debug.LogErrorFormat("{0} is an invalid string collection.", key);
        }
        return c;
    }

    #region Singleton Shits
    //////////////////////////////////////////////////////////////////////////////////////
    static private readonly StringDatabase instance = new StringDatabase();

    private StringDatabase()
    {

    }

    static public StringDatabase Instance
    {
        get
        {
            return instance;
        }
    }
    //////////////////////////////////////////////////////////////////////////////////////
    #endregion
}

} // end of namespace BadBird
