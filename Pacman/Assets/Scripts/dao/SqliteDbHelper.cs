using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System;
using System.Data;
public class SqliteDbHelper
{
    private SqliteConnection dbConnection;// 声明一个连接对象
    private SqliteCommand dbCommand;// 声明一个操作数据库命令
    private SqliteDataReader reader;// 声明一个读取结果集的一个或多个结果流

    public SqliteDbHelper(string connectionString)// 数据库的连接字符串，用于建立与特定数据源的连接
    {
        OpenDB(connectionString);
    }

    public void OpenDB(string connectionString)//连接数据库
    {
        try
        {
            dbConnection = new SqliteConnection(connectionString);
            dbConnection.Open();
            Debug.Log("Connected to db");
            Debug.Log(dbConnection);
        }
        catch (Exception e)
        {
            string temp1 = e.ToString();
            Debug.Log(temp1);
        }
    }

    public void CloseSqlConnection()// 关闭连接
    {
        if (dbCommand != null)
        {
            dbCommand.Dispose();
        }

        dbCommand = null;

        if (reader != null)
        {
            reader.Dispose();
        }

        reader = null;

        if (dbConnection != null)
        {
            dbConnection.Close();
        }

        dbConnection = null;

        Debug.Log("Disconnected from db.");
    }

    public SqliteDataReader ExecuteQuery(string sqlQuery)// 执行查询sqlite语句操作
    {
        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = sqlQuery;
        reader = dbCommand.ExecuteReader();
        return reader;
    }
    // 查询该表所有数据
    public SqliteDataReader ReadFullTable(string tableName)
    {
        string query = "SELECT * FROM " + tableName;
        return ExecuteQuery(query);
    }



    // 动态添加表字段到指定表  表名,字段集合
    //public SqliteDataReader InsertInto(string tableName, int score)
    //{
    //    string query = "INSERT INTO " + tableName + " VALUES ("  + score + ")";
    //    return ExecuteQuery(query);
    //}

    // 动态添加表字段到指定表  表名,字段集合
    public SqliteDataReader InsertInto(string tableName,int id, int score)
    {
        string query = "INSERT INTO " + tableName + " VALUES (" + id + ","+ score + ")";
        return ExecuteQuery(query);
    }



    // 动态更新表结构
    public SqliteDataReader UpdateInto(string tableName, string[] cols, string[] colsvalues, string selectkey, string selectvalue)
    {                                 //表名,字段集,对于集合值,要查询的字段,要查询的字段值
        string query = "UPDATE " + tableName + " SET " + cols[0] + " = " + colsvalues[0];
        for (int i = 1; i < colsvalues.Length; ++i)
        {
            query += ", " + cols[i] + " =" + colsvalues[i];
        }
        query += " WHERE " + selectkey + " = " + selectvalue + " ";
        return ExecuteQuery(query);
    }
    // 动态删除指定表字段数据
    public SqliteDataReader Delete(string tableName, string[] cols, string[] colsvalues)
    {                             //表名，字段，字段值
        string query = "DELETE FROM " + tableName + " WHERE " + cols[0] + " = " + colsvalues[0];
        for (int i = 1; i < colsvalues.Length; ++i)
        {
            query += " or " + cols[i] + " = " + colsvalues[i];
        }
        Debug.Log(query);
        return ExecuteQuery(query);
    }
    // 动态添加数据到指定表
    public SqliteDataReader InsertIntoSpecific(string tableName, string[] cols, string[] values)
    {
        if (cols.Length != values.Length)
        {
            throw new SqliteException("columns.Length != values.Length");
        }
        string query = "INSERT INTO " + tableName + "(" + cols[0];
        for (int i = 1; i < cols.Length; ++i)
        {
            query += ", " + cols[i];
        }
        query += ") VALUES (" + values[0];
        for (int i = 1; i < values.Length; ++i)
        {
            query += ", " + values[i];
        }
        query += ")";
        return ExecuteQuery(query);
    }
    // 动态删除表
    public SqliteDataReader DeleteContents(string tableName)
    {
        string query = "DELETE FROM " + tableName;
        return ExecuteQuery(query);
    }
    // 动态创建表
    public SqliteDataReader CreateTable(string name, string[] col, string[] colType)
    {                                   //表名，字段，字段值
        if (col.Length != colType.Length)
        {
            throw new SqliteException("columns.Length != colType.Length");
        }
        string query = "";

        return ExecuteQuery(query);
    }
    // 根据查询条件 动态查询数据信息
    public SqliteDataReader SelectWhere(string tableName, string[] items, string[] col, string[] operation, string[] values)
    {                                   //表名，查询数据集合，字段，操作，值
        if (col.Length != operation.Length || operation.Length != values.Length)
        {
            throw new SqliteException("col.Length != operation.Length != values.Length");
        }
        string query = "SELECT " + items[0];
        for (int i = 1; i < items.Length; ++i)
        {
            query += ", " + items[i];
        }
        query += " FROM " + tableName + " WHERE " + col[0] + operation[0] + "'" + values[0] + "' ";
        for (int i = 1; i < col.Length; ++i)
        {
            query += " AND " + col[i] + operation[i] + "'" + values[0] + "' ";
        }
        return ExecuteQuery(query);
    }
}