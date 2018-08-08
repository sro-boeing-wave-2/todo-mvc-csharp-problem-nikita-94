using System;
using Google_Keep.Models;
using Google_Keep.Controllers;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoAssignment.Tests
{
    public class UnitTestToDo
    {

        public Notes1Controller GetController()
        {
            var optionsBuilder = new DbContextOptionsBuilder<Google_KeepContext>();
            optionsBuilder.UseInMemoryDatabase<Google_KeepContext>(Guid.NewGuid().ToString());
            Google_KeepContext g_keep_context = new Google_KeepContext(optionsBuilder.Options);
            GetData(optionsBuilder.Options);
            return new Notes1Controller(g_keep_context);
            //    _controller = new Notes1Controller(g_keep_context);
            //    GetData(g_keep_context);
            //}
        }
        public void GetData(DbContextOptions<Google_KeepContext> options)
        {
            using (var g_keep_context = new Google_KeepContext(options))
            {
                var notes = new List<Notes>()
            {
                new Notes()
                {
                    id = 1,
                    title="My First Note",
                    plain_text="This is my plaintext",
                    IsPinned=true,
                    check=new List<Checklist>()
                    {
                        new Checklist()
                        {
                            checklist="checklist data 1"

                        }
                    },
                    label = new List<Label>()
                    {
                        new Label()
                        {
                            label ="Nikita"
                        }
                    }
                },
                new Notes()
                {
                    id = 2,
                    title="My First Note2",
                    plain_text="This is my plaintext2",
                    IsPinned=true,
                    check=new List<Checklist>()
                    {
                        new Checklist()
                        {
                            checklist="checklist data 1"

                        }
                    },
                    label = new List<Label>()
                    {
                        new Label()
                        {
                            label ="Nikita"
                        }
                    }
                },
                 new Notes()
                {
                    id = 3,
                    title="My First Note3",
                    plain_text="This is my plaintext3",
                    IsPinned=true,
                    check=new List<Checklist>()
                    {
                        new Checklist()
                        {
                            checklist="checklist data 1"

                        }
                    },
                    label = new List<Label>()
                    {
                        new Label()
                        {
                            label ="Nikita"
                        }
                    }
                }
            };
                g_keep_context.Notes.AddRange(notes);
                var CountOfEntitiesBeingTracked = g_keep_context.ChangeTracker.Entries().Count();
                g_keep_context.SaveChanges();

            }
        }
        [Fact]
        public async void TestGet()
        {
            var _controller = GetController();
            var res = await _controller.GetNotes("My First Note", "Nikita", true) as OkObjectResult;
            var OkObj = res.Value as List<Notes>;
            Console.WriteLine(OkObj.Count);
            Assert.Equal(1, OkObj.Count);
        }

        //[Fact]
        //public async void GetAllNotes()
        //{
        //    var _controller = GetController();
        //    var result = await _controller.GetAllNotes() as OkObjectResult;
        //    var okobj = result.Value as List<Notes>;
        //    Assert.Equal(3, okobj.Count);
        //}
        [Fact]
        public async Task PostNotes()
        {
            var note = new Notes
            {
                id = 5,
                title = "Asp",
                plain_text = "Hello eWorld",
                IsPinned = true,
                label = new List<Label>() { new Label { label = "Label_1" }, new Label { label = "Label_2" } },
                check = new List<Checklist>() { new Checklist { checklist = "1" } }

            };
            var _controller = GetController();
            var result = _controller.PostNotes(note).Result as CreatedAtActionResult;
            //Console.WriteLine("posted result"+result);
            //var okResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
            //var notes = okResult.Value.Should().BeAssignableTo<Notes>().Subject;
            var item = result.Value as Notes;
            Assert.Equal("Hello eWorld", item.plain_text);

        }
        [Fact]
        public async Task TestPut()
        {
            var notes = new Notes
            {
                id = 2,
                title = "Asp",
                plain_text = "Hello World",
                IsPinned = true,
                label = new List<Label>() { new Label { label = "Label_1" } },
                check = new List<Checklist>() { new Checklist { checklist = "1" } }
            };

            var _controller = GetController();
            var result = await _controller.PutNotes(2, notes);
            var resultAsOkObjectResult = result as OkObjectResult;
            var okResult = resultAsOkObjectResult.Value as Notes;
            Assert.Equal(2, okResult.id);
            // var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            Assert.Equal("Hello World", okResult.plain_text);

        }
        [Fact]
        public async Task DeleteTest()
        {
            var _controller = GetController();
            var result = _controller.DeleteNotes(1);
            Assert.True(result.IsCompletedSuccessfully);
            //var response = await _controller.DeleteNotes(1) as OkObjectResult;
            //var result = response.Value as Notes;
            //Assert.Equal(1, result.id);
            //Assert.Equal("Nikita", result.label[0].label);
        }
    }
}