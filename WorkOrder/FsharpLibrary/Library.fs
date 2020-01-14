namespace FsharpLibrary

//open System.Collections.Generic
//my test Module
module Say =
    let hello name =
        printfn "Hello %s" name
 
    let sum (val1:int, val2:int) = val1+val2

    let multiply (val1:int, val2:int) = val1*val2

    let mutable currentUser = ""
    let mutable currentPassword = ""
    
    
    
    type ILogin =
       abstract member IsValid: bool

       

    type Login(id:int, username:string, password:string) =
        member this.ID = id
        member this.Username = username
        member this.Password = password
        interface ILogin with 
            member this.IsValid = this.Username = currentUser && this.Password = currentPassword

    let mutable currentList:List<Login> = List<Login>.Empty
    let mutable currentStringList:List<string> = List<string>.Empty
    
    let AddToList (listan:List<string>) (element:string) = element::currentStringList;
///the Object Structures for the model
module ObjectStructures = 
    ///the object structure for the Task table
    type Task(id:int32, userID:int32, task:string, taskCodeID:int32, description:string, shared:bool)=
        member this.ID = id
        member this.UserID = userID
        member this.Task = task
        member this.TaskCodeID = taskCodeID
        member this.Description = description
        member this.Shared = shared
    
    ///the object structure for the User table
    type User(id:int32, username:string, password:string, name:string)=
        member this.ID = id
        member this.Username = username
        member this.Password = password
        member this.Name = name
    
    ///the object structure for the TaskCodes table
    type TaskCode(id:int32, name:string, description:string, colorCode:string)=
        member this.Name = name
        member this.Description = description
        member this.ColorCode = colorCode
        

    ///the object structure for the TaskSharing table
    type TaskSharing(id:int32, taskID:int32, userID:int32)=
        member this.ID = id
        member this.TaskID = taskID 
        member this.UserID = userID
    
    //database lists
    ///the list of task objects
    let mutable taskList:List<Task> = List<Task>.Empty
    ///the list of user objects
    let mutable userList:List<User>= List<User>.Empty;
    ///the list of taskCode objects
    let mutable taskCodeList:List<TaskCode> = List<TaskCode>.Empty;
    //might not need the taskSharingList as we will erase the need through the data colection
    ///the list for the taskSharing objects
    let mutable taskSharingList:List<TaskSharing> = List<TaskSharing>.Empty;

    //Filtered Lists
    ///the list of filtered taskSharings for a certain client
    let mutable taskSharingListFiltered:List<TaskSharing> = List<TaskSharing>.Empty;

    ///the function for adding an element to the end of the task list
    let AddTask (element:Task) = taskList <- List.append taskList [element]
    ///the function for adding an element at the beginning of the task list
    let AddTaskToBeginning (element:Task) = taskList <- element::taskList
    
    ///the function for adding an element to the end of the user list
    let AddUser (element:User) = userList <- List.append userList [element]
    ///the function for adding an element to the beginning of the user list
    let AddUserToBeginning (element:User) = userList <- element::userList

    ///the function for adding an element to the end of the taskCode list
    let AddTaskCode (element:TaskCode) = taskCodeList <- List.append taskCodeList [element]
    ///the function for adding an element to the end of the beginning of the taskCode list
    let AddTaskCodeToBeginning (element:TaskCode) = taskCodeList <- element::taskCodeList

    ///the function for adding an element to the end of the taskSharing list
    let AddTaskSharing (element:TaskSharing) = taskSharingList <- List.append taskSharingList [element]
    ///the function for adding an element to the beginning of the taskSharing list
    let AddTaskSharingToBeginning (element:TaskSharing) = taskSharingList <- element::taskSharingList
    
   

    open System.Linq
    
    ///this function will retrieve the sharing list for a certain user
    let getSharingListForUsers(userID:int32) = taskSharingListFiltered <- Seq.toList<TaskSharing>(taskSharingList.Where(fun x -> x.UserID = userID))

    //taskCodeList |> List.iter (fun x-> x.ID <- x.ID + 1)
    //