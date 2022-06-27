using System;
using System.Collections.Generic;
using System.Linq;
using Library.Entities;

namespace TODOApp
{
#pragma warning disable S1118 // Utility classes should not have public constructors
    public class Program
#pragma warning restore S1118 // Utility classes should not have public constructors
    {
        public static List CreateList(string listName)
        {
            if (string.IsNullOrEmpty(listName))
            {
                throw new ArgumentNullException(listName, "Name of the list cannot be empty or null");
            }
            List list = new List { listName = listName };
            return list;
        }
        public static Task CreateTask(int listID, string taskName, string taskDescription, DateTime dateTime, bool taskStatus)
        {
            if (listID <= 0)
            {
                throw new ArgumentException("List ID cannot be 0 or a negative number", nameof(listID));
            }
            if (string.IsNullOrEmpty(taskName))
            {
                throw new ArgumentNullException(taskName, "Task name cannot be empty or null");
            }
            if(dateTime < DateTime.Now && !taskStatus)
            {
                throw new ArgumentException("The due date must be in the future if the task is not completed", nameof(dateTime));
            }
            Task task = new Task { listId = listID, taskName = taskName, taskDescription = taskDescription, dateTime = dateTime, taskStatus = taskStatus };
            return task;
        }
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List list;
                Task task;
                string output = "";
                while(output.ToLower() != "exit")
                {
                    Console.WriteLine("1. Add new TODO list\r\n2. View TODO lists\r\n3. Delete whole TODO list and assigned tasks\r\nType 'exit' to leave");
                    output = Console.ReadLine();
                    switch (output)
                    {
                        case "1":
                            Console.WriteLine("Add TODO list name:");
                            string listName = Console.ReadLine();
                            list = CreateList(listName);
                            db.Lists.AddRange(list);
                            db.Lists.Add(list);
                            db.SaveChanges();
                            break;
                        case "2":
                            Console.WriteLine("Do you want to see lists that tasks are already completed? Y/N");
                            string decision = Console.ReadLine();
                            if(decision.ToLower() == "y")
                            {
                                var lists = db.Lists.ToList();
                                foreach (List i in lists)
                                {
                                    Console.WriteLine($"{i.listId} -- {i.listName}");
                                }
                            }
                            else
                            {
                                var result =
                                    from t in db.Tasks
                                    where !t.taskStatus
                                    select t.listId;
                                var result1 =
                                    from l in db.Lists
                                    from r in result
                                    where l.listId == r
                                    select l;
                                foreach(var i in result1)
                                {
                                    Console.WriteLine($"{i.listId} -- {i.listName}");
                                }
                            }
                            Console.WriteLine("\r\nChoose a listID to see tasks and add new/modify tasks or type 'exit' to leave");
                            decision = Console.ReadLine();
                            if (decision.ToLower() == "exit")
                            {
                                break;
                            }
                            else
                            {
                                var result = db.Tasks.Where(task => task.listId == Int32.Parse(decision)).ToList();
                                foreach (var i in result)
                                {
                                    Console.WriteLine($"ID: {i.taskId}, Task Name: {i.taskName}, Due Date: {i.dateTime}, Task Status: {i.taskStatus}\r\n{i.taskDescription}");
                                }
                                string output2 = "";
                                while(output2.ToLower() != "exit")
                                {
                                    Console.WriteLine("\r\n1. Modify existing task\r\n2. Add new task to list\r\n3. Delete task\r\nType exit to leave");
                                    output2 = Console.ReadLine();
                                    switch (output2)
                                    {
                                        case "1":
                                            Console.WriteLine("Type task ID to modify that task");
                                            string stringTaskID = Console.ReadLine();
                                            Console.WriteLine("Type task name");
                                            string stringTaskName = Console.ReadLine();
                                            Console.WriteLine("Type task description");
                                            string stringTaskDescription = Console.ReadLine();
                                            Console.WriteLine("Type task due date(Date format: yyyy.mm.dd)");
                                            string stringTaskDateTime = Console.ReadLine();
                                            Console.WriteLine("Type task status (true = done false = undone)");
                                            string stringTaskStatus = Console.ReadLine();

                                            var change = db.Tasks.Where(task => task.taskId == Int32.Parse(stringTaskID)).FirstOrDefault();
                                            if (change != null)
                                            {
                                                change.taskName = stringTaskName;
                                                change.taskDescription = stringTaskDescription;
                                                change.dateTime = DateTime.Parse(stringTaskDateTime);
                                                change.taskStatus = (stringTaskStatus.ToLower() == "true");
                                                db.SaveChanges();
                                            }
                                            Console.WriteLine("Task with ID: {0} modified", stringTaskID);
                                            break;
                                        case "2":
                                            Console.WriteLine("Type task name");
                                            stringTaskName = Console.ReadLine();
                                            Console.WriteLine("Type task description");
                                            stringTaskDescription = Console.ReadLine();
                                            Console.WriteLine("Type task due date(Date format: yyyy.mm.dd)");
                                            stringTaskDateTime = Console.ReadLine();
                                            Console.WriteLine("Type task status (true = done false = undone)");
                                            stringTaskStatus = Console.ReadLine();
                                            task = CreateTask(Int32.Parse(decision), stringTaskName, stringTaskDescription, DateTime.Parse(stringTaskDateTime), (stringTaskStatus.ToLower() == "true"));
                                            db.Tasks.AddRange(task);
                                            db.Tasks.Add(task);
                                            db.SaveChanges();
                                            Console.WriteLine("New task created!");
                                            break;
                                        case "3":
                                            Console.WriteLine("Type ID of task that you want to delete");
                                            stringTaskID = Console.ReadLine();
                                            var taskToRemove = db.Tasks.SingleOrDefault(task => task.taskId == Int32.Parse(stringTaskID));
                                            if (taskToRemove != null)
                                            {
                                                db.Tasks.Remove(taskToRemove);
                                                db.SaveChanges();
                                            }
                                            Console.WriteLine("Task with ID: {0} has been deleted", stringTaskID);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                            break;
                        case "3":
                            Console.WriteLine("Type list ID to delete it");
                            string stringListID = Console.ReadLine();
                            var tasksToDelete = db.Tasks.Where(task => task.listId == Int32.Parse(stringListID));
                            var listToDelete = db.Lists.SingleOrDefault(list => list.listId == Int32.Parse(stringListID));
                            if (tasksToDelete != null)
                            {
                                foreach (Task i in tasksToDelete)
                                {
                                    db.Tasks.Remove(i);
                                }
                                db.SaveChanges();
                            }
                            if (listToDelete != null)
                            {
                                db.Lists.Remove(listToDelete);
                                db.SaveChanges();
                            }
                            Console.WriteLine("List with ID: {0} and all assigned to this list tasks had been deleted", stringListID);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}

