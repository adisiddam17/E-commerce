using HRApp.Entities;
using MySql.Data.MySqlClient;
namespace HRApp.Repository;

using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
public class EmployeesRepository:IEmployeesRepository{
    private MySqlConnection conn;
    private MySqlCommand cmd;
    public EmployeesRepository()
    {
        this.conn=new MySqlConnection();
        conn.ConnectionString=@"server=localhost; port=3307; user=root; password=password; database=dotnet";
        this.cmd=new MySqlCommand();
        cmd.Connection=conn;
    }
    
    public List<Employee> getAll(){
        List<Employee> employees=new List<Employee>();
        cmd.CommandText="SELECT * FROM employees";        
        try{
            conn.Open();

        MySqlDataReader reader=cmd.ExecuteReader();
        while(reader.Read())
        {
            int id=int.Parse(reader["Id"].ToString());
            string? name=reader["Name"].ToString();
            double basicSal=double.Parse(reader["BasicSalary"].ToString());
            double dailyAllo=double.Parse(reader["DailyAllowance"].ToString());    
            double tax=double.Parse(reader["Tax"].ToString()); 
            int noOFWorkingDays=int.Parse(reader["NoOFWorkingDays"].ToString());

            employees.Add(new Employee(id,name,basicSal,dailyAllo,tax,noOFWorkingDays));
        }
        reader.Close();
        }catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }finally{
            conn.Close();
        }
        return employees;
    }

    public Employee getById(int id){
        cmd.CommandText="SELECT * FROM employees WHERE Id="+id;
        Employee emp=new Employee();
        try{
            conn.Open();
            MySqlDataReader reader=cmd.ExecuteReader();
            Console.WriteLine("reader : "+reader);
        if(reader.Read())
        {
            int empId=int.Parse(reader["Id"].ToString());
            string? name=reader["Name"].ToString();
            double basicSal=double.Parse(reader["BasicSalary"].ToString());
            double dailyAllo=double.Parse(reader["DailyAllowance"].ToString());    
            double tax=double.Parse(reader["Tax"].ToString()); 
            int noOFWorkingDays=int.Parse(reader["NoOFWorkingDays"].ToString());
            emp.Id=empId;
            emp.Name=name;
            emp.BasicSal=basicSal;
            emp.DailyAllo=dailyAllo;
            emp.Tax=tax;
            emp.NoOfWorkingDays=noOFWorkingDays;
        }
        reader.Close();
        }catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }finally{
            conn.Close();
        }
        Console.WriteLine("In repo "+emp);
        return emp;
    }
    public string insert(Employee emp){
        string mesg="";
        cmd.CommandText="Insert into employees values(default,@Name,@BasicSalary,@DailyAllowance,@Tax,@NoOfWorkingDays)";
        cmd.Parameters.AddWithValue("@Name",emp.Name);
        cmd.Parameters.AddWithValue("@BasicSalary", emp.BasicSal);
        cmd.Parameters.AddWithValue("@DailyAllowance", emp.DailyAllo);
        cmd.Parameters.AddWithValue("@Tax", emp.Tax);
        cmd.Parameters.AddWithValue("@NoOfWorkingDays", emp.NoOfWorkingDays);
        try{
            conn.Open();
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            mesg="Data inserted successfully";
        }catch(Exception e)
        {
            Console.WriteLine(e.Message);
            mesg="Data cannot be inserted";
        }finally{
            conn.Close();
        }
        return mesg;
    }
    public string update(Employee emp){
        string mesg="";
        // cmd.CommandText=$"Update employees set Name={emp.Name} , BasicSalary={emp.BasicSal}, DailyAllowance={emp.DailyAllo},Tax={emp.Tax}, NoOfWorkingDays={emp.NoOfWorkingDays} where Id={emp.Id}";
        cmd.CommandText = "UPDATE employees SET Name=@Name, BasicSalary=@BasicSalary, DailyAllowance=@DailyAllowance, Tax=@Tax, NoOfWorkingDays=@NoOfWorkingDays WHERE Id=@Id";

        //clears any existing parameters..if any
        cmd.Parameters.Clear();

        // Add parameters to the SqlCommand
        cmd.Parameters.AddWithValue("@Name", emp.Name);
        cmd.Parameters.AddWithValue("@BasicSalary", emp.BasicSal);
        cmd.Parameters.AddWithValue("@DailyAllowance", emp.DailyAllo);
        cmd.Parameters.AddWithValue("@Tax", emp.Tax);
        cmd.Parameters.AddWithValue("@NoOfWorkingDays", emp.NoOfWorkingDays);
        cmd.Parameters.AddWithValue("@Id", emp.Id);
        try{
            conn.Open();
            cmd.ExecuteNonQuery();
            mesg="Data Updated successfully";
        }catch(Exception e)
        {
            Console.WriteLine(e.Message);
            mesg="Data cannot be updated";
        }finally{
            conn.Close();
        }
        return mesg;
    }
    public string delete(int id){
        string mesg="";
        cmd.CommandText=$"DELETE from employees where Id={id}";
        try{
            conn.Open();
            cmd.ExecuteNonQuery();
            mesg="Data deleted successfully";
        }catch(Exception e)
        {
            Console.WriteLine(e.Message);
            mesg="Data cannot be deleted";
        }finally{
            conn.Close();
        }
        return mesg;
    }
}