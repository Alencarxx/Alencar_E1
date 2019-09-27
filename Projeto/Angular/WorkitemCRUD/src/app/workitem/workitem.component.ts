import { Component, OnInit } from '@angular/core';  
import { FormBuilder, Validators } from '@angular/forms';  
import { Observable } from 'rxjs';  
import { WorkitemService } from '../workitem.service';  
import { Workitem} from '../workitem';  
  
@Component({  
  selector: 'app-workitem',  
  templateUrl: './workitem.component.html',  
  styleUrls: ['./workitem.component.css']  
})  
export class WorkitemComponent implements OnInit {  
  dataSaved = false;  
  workitemForm: any;  
  allWorkitems: Observable<Workitem[]>;  
  workitemIdUpdate = null;  
  massage = null;  
  
  constructor(private formbulider: FormBuilder, private employeeService:WorkitemService) { }  
  
  ngOnInit() {  
    this.workitemForm = this.formbulider.group({  
      ID: ['', [Validators.required]],  
      DataCriacao: ['', [Validators.required]],  
      Tipo: ['', [Validators.required]],  
      Titulo: ['', [Validators.required]],
    });  
    this.loadAllWorkitems();  
  }  
  loadAllWorkitems() {  
    this.allWorkitems = this.WorkitemService.getAllWorkitem();  
  }  
  onFormSubmit() {  
    this.dataSaved = false;  
    const workitem = this.workitemForm.value;  
    this.CreateWorkitem(workitem);  
    this.workitemForm.reset();  
  }  
  loadWorkitemToEdit(workitemId: string) {  
    this.workitemService.getWorkitemById(workitemId).subscribe(workitem=> {  
      this.massage = null;  
      this.dataSaved = false;  
      this.workitemIdUpdate = workitem.ID;  
      this.workitemForm.controls['ID'].setValue(workitem.ID);  
     this.workitemForm.controls['DataCriacao'].setValue(workitem.DataCriacao);  
      this.workitemForm.controls['Tipo'].setValue(workitem.Tipo);  
      this.workitemForm.controls['Titulo'].setValue(workitem.Titulo); 
    
    });  
  
  }  
  CreateWorkitem(workitem: Workitem) {  
    if (this.workitemdUpdate == null) {  
      this.workitemService.createWorkitem(workitem).subscribe(  
        () => {  
          this.dataSaved = true;  
          this.massage = 'Record saved Successfully';  
          this.loadAllWorkitems();  
          this.workitemIdUpdate = null;  
          this.workitemForm.reset();  
        }  
      );  
    } else {  
      workitem.ID = this.workitemdUpdate;  
      this.workitemService.updateWorkitem(workitem).subscribe(() => {  
        this.dataSaved = true;  
        this.massage = 'Record Updated Successfully';  
        this.loadAllWorkitems();  
        this.workitemIdUpdate = null;  
        this.workitemForm.reset();  
      });  
    }  
  }   
  deleteWorkitem(workitemId: string) {  
    if (confirm("Are you sure you want to delete this ?")) {   
    this.workitemService.deleteWorkitemById(workitemId).subscribe(() => {  
      this.dataSaved = true;  
      this.massage = 'Record Deleted Succefully';  
      this.loadAllWorkitems();  
      this.workitemIdUpdate = null;  
      this.workitemForm.reset();  
  
    });  
  }  
}  
  resetForm() {  
    this.workitemForm.reset();  
    this.massage = null;  
    this.dataSaved = false;  
  }  
}  