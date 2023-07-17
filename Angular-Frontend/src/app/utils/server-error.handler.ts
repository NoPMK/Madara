import { HttpErrorResponse } from "@angular/common/http";
import { FormGroup } from "@angular/forms";

export function serverErrorHandler(err: Error, form: FormGroup): void{
    if(err instanceof HttpErrorResponse && err.status===400 && err.error.errors){
        const errorMessegeObj = err.error.errors;

        Object.keys(errorMessegeObj)
        .forEach(key=>{
  
          const controlKey= key.charAt(0).toLowerCase()+ key.slice(1);
          //const controlkey2=key.charAt(0).toLowerCase()+key.slice(0)
          
          if(form.controls[controlKey]){
            form.controls[controlKey].setErrors({missingField:true, message:errorMessegeObj[key]})
          }
  
        })  
    }
}