import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ChatbotService, ChatMessage, Conversation, QuickReply } from '../../../core/services/chatbot.service';
import { AuthService } from '../../../core/services/auth.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { Subscription } from 'rxjs';
import { FormatMessagePipe } from "../../../core/services/format-message.pipe";

@Component({
  selector: 'app-chatbot',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatButtonModule,
    MatTooltipModule,
    FormatMessagePipe
],
  templateUrl: './chatbot.component.html',
  styleUrls: ['./chatbot.component.css']
})
export class ChatbotComponent implements OnInit {
  messages: ChatMessage[] = [];
  newMessage = '';
  conversation?: Conversation;
  isLoading = false;
  isSending = false;
  @Output() chatClosed = new EventEmitter<void>();
  private subscriptions = new Subscription();

  constructor(
    private chatbotService: ChatbotService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.startConversation();
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  startConversation(): void {
    this.isLoading = true;
    this.subscriptions.add(
      this.chatbotService.startConversation().subscribe({
        next: (conversation) => {
          this.conversation = conversation;
          this.loadMessages(conversation.id);
        },
        error: (err) => {
          console.error('Failed to start conversation', err);
          this.isLoading = false;
        }
      })
    );
  }

  loadMessages(conversationId: number): void {
    this.subscriptions.add(
      this.chatbotService.loadInitialMessages(conversationId).subscribe({
        next: (messages) => {
          this.messages = messages;
          this.isLoading = false;
          this.scrollToBottom();
        },
        error: (err) => {
          console.error('Failed to load messages', err);
          this.isLoading = false;
        }
      })
    );
  }

  sendMessage(): void {
    if (!this.newMessage.trim() || !this.conversation || this.isSending) return;

    this.isSending = true;
    const messageContent = this.newMessage;
    this.newMessage = '';

    // إضافة الرسالة مباشرة للواجهة قبل الإرسال
    this.messages.push({
      id: 0, // مؤقت
      conversationId: this.conversation.id,
      content: messageContent,
      timestamp: new Date().toISOString(),
      sender: 'User',
      type: 'Text'
    });

    this.scrollToBottom();

    this.subscriptions.add(
      this.chatbotService.sendMessage(this.conversation.id, messageContent).subscribe({
        next: (newMessages) => {
          this.messages = newMessages;
          this.isSending = false;
          this.scrollToBottom();
        },
        error: (err) => {
          console.error('Failed to send message', err);
          this.isSending = false;
        }
      })
    );
  }

  handleQuickReply(reply: QuickReply): void {
    this.newMessage = reply.payload;
    this.sendMessage();
  }

  closeChat(): void {
    if (this.conversation) {
      this.subscriptions.add(
        this.chatbotService.closeConversation(this.conversation.id).subscribe({
          next: () => {
            this.chatClosed.emit();
          },
          error: (err) => console.error('Error closing conversation:', err)
        })
      );
    } else {
      this.chatClosed.emit();
    }
  }

  isBotMessage(message: ChatMessage): boolean {
    return message.sender === 'Bot' || message.sender === 'System';
  }

  trackByMessageId(index: number, message: ChatMessage): number {
    return message.id;
  }

  private scrollToBottom(): void {
    setTimeout(() => {
      const messagesContainer = document.querySelector('.chat-messages');
      if (messagesContainer) {
        messagesContainer.scrollTop = messagesContainer.scrollHeight;
      }
    }, 100);
  }
}