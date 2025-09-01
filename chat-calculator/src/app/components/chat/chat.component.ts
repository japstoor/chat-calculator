import { Component } from '@angular/core';
import { ChatService } from '../../services/chat.service';
import { AuthService } from '../../services/auth.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
interface Message {
  sender: 'user' | 'bot';
  text: string;
}

@Component({
  selector: 'app-chat',
  standalone:true,
  imports:[
    CommonModule,
    HttpClientModule,
    FormsModule
  ],
  templateUrl: './chat.component.html'
})
export class ChatComponent {
  messages: Message[] = [];
  input = '';
  user: any;

  constructor(private chatService: ChatService, private authService: AuthService) {
    this.user = this.authService.getUser();
  }

  send() {
    if (!this.input.trim()) return;

    // User message
    this.messages.unshift({ sender: 'user', text: this.input });

    // Call API
    this.chatService.calculate(this.user.userId, this.input).subscribe((res) => {
      this.messages.unshift({ sender: 'bot', text: `${res.resultNumber} (${res.resultWords})` });
    });

    this.input = '';
  }
}
