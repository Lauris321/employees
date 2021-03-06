﻿using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Models
{
    public class EmployeeContext
    {
        public string ConnectionString { get; set; }

        public EmployeeContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> list = new List<Employee>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM employees", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Employee()
                        {
                            ID = reader.GetInt32("ID"),
                            Name = reader.GetString("name"),
                            LastName = reader.GetString("lastname"),
                            NetWage = reader.GetDecimal("net_wage"),
                        });
                    }
                }
            }

            return list;
        }

        public List<Employee> GetSomeEmployees(int? page)
        {
            List<Employee> list = new List<Employee>();
            int? from = page * 10;
            int to = 10;

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM employees LIMIT {from}, {to}", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Employee()
                        {
                            ID = reader.GetInt32("ID"),
                            Name = reader.GetString("name"),
                            LastName = reader.GetString("lastname"),
                            NetWage = reader.GetDecimal("net_wage"),
                        });
                    }
                }
            }

            return list;
        }

        public List<Employee> Search(string searchString)
        {
            List<Employee> list = new List<Employee>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM employees WHERE name='{searchString}' OR lastname='{searchString}'", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Employee()
                        {
                            ID = reader.GetInt32("ID"),
                            Name = reader.GetString("name"),
                            LastName = reader.GetString("lastname"),
                            NetWage = reader.GetDecimal("net_wage"),
                        });
                    }
                }
            }

            return list;
        }

        public Employee GetEmployee(int? id)
        {
            Employee employee = new Employee();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM employees WHERE ID={id}", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employee = new Employee()
                        {
                            ID = reader.GetInt32("ID"),
                            Name = reader.GetString("name"),
                            LastName = reader.GetString("lastname"),
                            NetWage = reader.GetDecimal("net_wage"),
                        };
                    }
                }
            }

            return employee;
        }

        public void Create(Employee employee)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                NumberFormatInfo nfi = new NumberFormatInfo();
                nfi.NumberDecimalSeparator = ".";
                MySqlCommand cmd = new MySqlCommand($"INSERT INTO employees (name, lastname, net_wage) VALUES('{employee.Name}', '{employee.LastName}', {employee.NetWage.ToString(nfi)})", conn);
                
                cmd.ExecuteScalar();
            }
        }

        public void Edit(Employee employee)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                NumberFormatInfo nfi = new NumberFormatInfo();
                nfi.NumberDecimalSeparator = ".";
                MySqlCommand cmd = new MySqlCommand($"UPDATE employees SET name='{employee.Name}', lastname='{employee.LastName}', net_wage='{employee.NetWage.ToString(nfi)}' WHERE ID='{employee.ID}'", conn);

                cmd.ExecuteScalar();
            }
        }

        public void Delete(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                NumberFormatInfo nfi = new NumberFormatInfo();
                nfi.NumberDecimalSeparator = ".";
                MySqlCommand cmd = new MySqlCommand($"DELETE FROM employees WHERE ID={id}", conn);

                cmd.ExecuteScalar();
            }
        }
    }

    public class Employee
    {
        private EmployeeContext context;

        public int ID { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string LastName { get; set; }

        public decimal NetWage { get; set; }
    }
}
