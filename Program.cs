using System;
using System.Collections.Generic;
using LibraryData;

namespace LibraryApp
{
    class Program
    {
        static BookRepository bookRepo = new BookRepository();
        static MemberRepository memberRepo = new MemberRepository();
        static BackupHelper backupHelper = new BackupHelper();

        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
             
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. View All Books");
                Console.WriteLine("3. Add Member");
                Console.WriteLine("4. View All Members");
                Console.WriteLine("5. Issue Book to Member (Transaction)");
                Console.WriteLine("6. Return Book (Transaction)");
                Console.WriteLine("7. Export Books to JSON");
                Console.WriteLine("8. Import Books from JSON");
                Console.WriteLine("9. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        ViewAllBooks();
                        break;
                    case "3":
                        AddMember();
                        break;
                    case "4":
                        ViewAllMembers();
                        break;
                    case "5":
                        IssueBook();
                        break;
                    case "6":
                        ReturnBook();
                        break;
                    case "7":
                        ExportBooks();
                        break;
                    case "8":
                        ImportBooks();
                        break;
                    case "9":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        static void AddBook()
        {
            Console.Write("Title: ");
            string title = Console.ReadLine();
            Console.Write("Author: ");
            string author = Console.ReadLine();
            Console.Write("Stock: ");
            int stock = int.Parse(Console.ReadLine());

            Book book = new Book { Title = title, Author = author, Stock = stock };
            bookRepo.AddBook(book);
            Console.WriteLine("Book added.");
        }

        static void ViewAllBooks()
        {
            List<Book> books = bookRepo.GetAllBooks();
            foreach (Book b in books)
            {
                Console.WriteLine($"{b.BookId} | {b.Title} | {b.Author} | Stock: {b.Stock}");
            }
        }

        static void AddMember()
        {
            Console.Write("Name: ");
            string name = Console.ReadLine();

            Member member = new Member { Name = name };
            memberRepo.AddMember(member);
            Console.WriteLine("Member added.");
        }

        static void ViewAllMembers()
        {
            List<Member> members = memberRepo.GetAllMembers();
            foreach (Member m in members)
            {
                Console.WriteLine($"{m.MemberId} | {m.Name}");
            }
        }

        static void IssueBook()
        {
            Console.Write("Book ID: ");
            int bookId = int.Parse(Console.ReadLine());
            Console.Write("Member ID: ");
            int memberId = int.Parse(Console.ReadLine());

            bool success = bookRepo.IssueBook(bookId, memberId);
            Console.WriteLine(success ? "Book issued successfully." : "Issue failed (no stock or invalid book).");
        }

        static void ReturnBook()
        {
            Console.Write("Issue ID: ");
            int issueId = int.Parse(Console.ReadLine());

            bool success = bookRepo.ReturnBook(issueId);
            Console.WriteLine(success ? "Book returned successfully." : "Return failed (invalid or already returned).");
        }

        static void ExportBooks()
        {
            Console.Write("File path to save (e.g. C:\\backup\\books.json): ");
            string path = Console.ReadLine();

            List<Book> books = bookRepo.GetAllBooks();
            backupHelper.SaveBooksToJson(books, path);
            Console.WriteLine("Books exported.");
        }

        static void ImportBooks()
        {
            Console.Write("File path to load from: ");
            string path = Console.ReadLine();

            List<Book> books = backupHelper.LoadBooksFromJson(path);
            foreach (Book b in books)
            {
                Console.WriteLine($"{b.Title} - Stock: {b.Stock}");
            }
        }
    }
}