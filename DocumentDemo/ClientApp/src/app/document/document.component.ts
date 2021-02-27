import { Component, OnInit } from '@angular/core';
import { DocumentService } from '../Services/document.service';
import { Document } from '../Models/Document';
import { DocumentList } from '../Models/DocumentList';

@Component({
  selector: 'app-document',
  templateUrl: './document.component.html',
  styleUrls: ['./document.component.css']
})
export class DocumentComponent implements OnInit {

  UploadedFiles: any[] = [];
  Documents: Document[] = [];
  MaxFileSize: number = 1048576;
  DocListToShow: any;

  constructor(private DocumentService: DocumentService) { }

  ngOnInit() {
  }

  handleFileInput(event) {
    //clear existing added files
    this.UploadedFiles = [];
    this.Documents = [];

    if (event.target.files && event.target.files.length > 0 && event.target.files.length < 6) {
      //user can add upto 5 files
      for (let index = 0; index < event.target.files.length; index++) {
        if (event.target.files[index].size < this.MaxFileSize) {
          // Add file to list of files if file is less then 1 MB
          this.UploadedFiles.push(event.target.files[index]);
        }
        else {
          alert('File size should be less than 1 MB');
        }
      }
    }
    else {
      alert('Please enter minimum 1 and maximum 5 files');
    }
    for (let index = 0; index < this.UploadedFiles.length; index++) {
      let Doc = new Document();
      //Setting Document information
      Doc.DocumentName = this.UploadedFiles[index].name;
      Doc.ContentType = this.UploadedFiles[index].type;
      let fileReader = new FileReader();
      fileReader.onload = () => {
        //pushing Document in the Documents array once file is uploaded
        Doc.DocumentContent = fileReader.result.toString();
        this.Documents.push(Doc);
      }
      // Reading data from file to Filereader
      fileReader.readAsDataURL(this.UploadedFiles[index]);
    }
  }

  onSubmit() {
    if (this.Documents.length > 0) {
      this.DocumentService.UploadDocument(new DocumentList(this.Documents)).subscribe(
        data => {
          this.UploadedFiles = [];
          this.Documents = [];
          alert(data.message);
          this.DocListToShow = data.Data;
        },
        error => {
          this.UploadedFiles = [];
          this.Documents = [];
          alert(error);
        });
    }
    else {
      alert("please enter document first.");
    }
  }

}
