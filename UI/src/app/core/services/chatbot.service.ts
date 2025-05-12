import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { environment } from '../../environment';
import { tap } from 'rxjs/operators';

export interface Conversation {
  id: number;
  userId: string;
  startDate: string;
  endDate?: string;
  status: 'Active' | 'Closed';
  lastMessageDate?: string;
}

export interface ChatMessage {
  id: number;
  conversationId: number;
  content: string;
  timestamp: string;
  sender: 'User' | 'Bot' | 'System';
  type: 'Text' | 'TrackingUpdate' | 'QuickReplies';
  quickReplies?: QuickReply[];
}

export interface QuickReply {
  title: string;
  payload: string;
}

@Injectable({
  providedIn: 'root'
})
export class ChatbotService {
  private apiUrl = `${environment.apiUrl}/api/chatbot`;
  private currentConversation = new BehaviorSubject<Conversation | null>(null);
  private newMessageCount = new BehaviorSubject<number>(0);

  constructor(private http: HttpClient) { }

  get currentConversation$(): Observable<Conversation | null> {
    return this.currentConversation.asObservable();
  }

  get newMessageCount$(): Observable<number> {
    return this.newMessageCount.asObservable();
  }

  startConversation(): Observable<Conversation> {
    return this.http.post<Conversation>(`${this.apiUrl}/start`, {}).pipe(
      tap(conversation => {
        this.currentConversation.next(conversation);
        this.newMessageCount.next(0);
      })
    );
  }

  sendMessage(conversationId: number, content: string): Observable<ChatMessage[]> {
    return this.http.post<ChatMessage[]>(
      `${this.apiUrl}/${conversationId}/messages`, 
      { content }
    ).pipe(
      tap(messages => {
        if (!this.currentConversation.value) return;
        this.newMessageCount.next(this.newMessageCount.value + 1);
      })
    );
  }

  getMessages(conversationId: number): Observable<ChatMessage[]> {
    return this.http.get<ChatMessage[]>(`${this.apiUrl}/${conversationId}/messages`);
  }

  closeConversation(conversationId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${conversationId}`).pipe(
      tap(() => {
        this.currentConversation.next(null);
        this.newMessageCount.next(0);
      })
    );
  }

  loadInitialMessages(conversationId: number): Observable<ChatMessage[]> {
    return this.getMessages(conversationId);
  }
}