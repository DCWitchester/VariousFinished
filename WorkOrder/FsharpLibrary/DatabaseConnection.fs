namespace FsharpLibrary

open System
open Npgsql.FSharp
open Npgsql.FSharp.OptionWorkflow

module DatabaseObjects = 
    type User ={
        ID : int32
        Username : string
        Password : string
        Name : string
        }
module DatabaseConnection =

    //the qualifier acces entrace
    [<RequireQualifiedAccess>]
    type SqlValue =
        | Null
        | Short of int16
        | Int of int
        | Long of int64
        | String of string
        | Date of DateTime
        | Bool of bool
        | Number of double
        | Decimal of decimal
        | Bytea of byte[]
        | HStore of Map<string, string>
        | Uuid of Guid
        | Timestamp of DateTime
        | TimestampWithTimeZone of DateTime
        | Time of TimeSpan
        | TimeWithTimeZone of DateTimeOffset
        | Jsonb of string

    // A row is a list of key/value pairs
    type SqlRow = list<string * SqlValue>

    // A table is list of rows
    type SqlTable = list<SqlRow>

    //Connection String
    ///the default connection towards the database: for use when no other connection is given
    let defaultConnectionSettings = 
        Sql.host "5.2.228.239"
        |> Sql.port 26662
        |> Sql.username "postgres"
        |> Sql.password "pgsql"
        |> Sql.database "MentorWorkOrder"
        |> Sql.sslMode SslMode.Prefer
    
    ///the default connection string will be instantiated with a
    let mutable defaultConnection = defaultConnectionSettings

    let setConnectionFromUri (uri:string) = defaultConnection <- Sql.fromUriToConfig(Uri uri)

    module UserFunctuins =

        let insertUser(user: DatabaseObjects.User) =
            defaultConnection 
            |> Sql.connectFromConfig
            |> Sql.query "INSERT INTO utilizatori(nume_utilizator,parola,denumire_utilizator) 
                                VALUES(@numeUtilizator,@parola,@denumireUtilizator) 
                                ON CONFLICT utilizatori_nume_utilizator_key 
                                DO UPDATE SET parola = @parola, denumire_utilizator = @denumireUtilizator, activ = true"
            |> Sql.parameters
                [
                    "numeUtilizator", Sql.Value user.Username
                    "parola", Sql.Value user.Password
                    "denumire_utilizator", Sql.Value user.Name
                ]
            |> Sql.prepare
            |> Sql.executeNonQuery

        let removeUser(user: DatabaseObjects.User) =
            defaultConnection
            |> Sql.connectFromConfig
            |> Sql.query "UPDATE utilizatori SET activ = false WHERE id = @ID"
            |> Sql.parameters
                [
                    "ID", Sql.Value user.ID
                ]
            |> Sql.prepare
            |> Sql.executeNonQuery

        let changeUserConnection(user: DatabaseObjects.User, loggedState: Boolean) =
            defaultConnection
            |> Sql.connectFromConfig
            |> Sql.query "UPDATE utilizatori SET is_logged = @loggedState WHERE id = @utilizator_id"
            |> Sql.parameters
                [
                    "utilizator_id", Sql.Value user.ID
                    "loggedState", Sql.Value loggedState
                ]
            |> Sql.prepare
            |> Sql.executeNonQuery

        let getAllUsers() : DatabaseObjects.User list = 
            defaultConnection
            |> Sql.connectFromConfig
            |> Sql.query "SELECT * FROM utilizatori"
            |> Sql.prepare
            |> Sql.executeTable
            |> Sql.mapEachRow (fun row ->
                option {
                    let! id = Sql.readInt "id" row
                    let! username = Sql.readString "nume_utilizator" row
                    let! password = Sql.readString "parola" row
                    let! name = Sql.readString "denumire_utilizator" row
                    return {ID = id; Username = username; Password = password; Name = name}
                })

        let checkUser(user:DatabaseObjects.User) : bool = 
            defaultConnection
                |> Sql.connectFromConfig
                |> Sql.query "SELECT COUNT(*) FROM utilizatori WHERE nume_utilizator = @numeUtilizator AND  parola = @parola AND activ"
                |> Sql.parameters
                    [   
                        "numeUtilizator", Sql.Value user.Username
                        "parola", Sql.Value user.Password
                    ]
                |> Sql.prepare
                |> Sql.executeScalar
                |> Sql.toLong
                |> function 
                    |  value ->
                        if(value > (int64 0)) 
                            then true
                            else false