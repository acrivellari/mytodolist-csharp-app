using CommunityToolkit.Mvvm.Messaging.Messages;
using MyToDoList.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDoList.Models;

public class NavigationRequestMessage(PageNames page) : ValueChangedMessage<PageNames>(page);
