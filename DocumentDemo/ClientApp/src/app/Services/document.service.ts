import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { DocumentList } from '../Models/DocumentList';

@Injectable({
  providedIn: 'root'
})
export class DocumentService {
  BaseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.BaseUrl = baseUrl;
  }

  UploadDocument(DocumentList: DocumentList) {
    return this.http.post<any>(this.BaseUrl + 'api/Document/DocumentUpload', DocumentList);
  }
}
