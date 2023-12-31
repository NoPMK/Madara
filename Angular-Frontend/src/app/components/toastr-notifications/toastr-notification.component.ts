import { Component, OnInit } from '@angular/core';  
  
import { Notification, NotificationType } from "./toastr-notification.model";  
import { NotificationService } from "./toastr-notification.service";  
  
@Component({  
    selector: 'toastr-notification',  
    templateUrl: './toastr-notification.component.html',  
    styleUrls: ['./toastr-notification.component.css' ] 
})  
  
export class NotificationComponent {  
    notifications: Notification[] = [];  
  
    constructor(public _notificationService: NotificationService) { }  
  
    ngOnInit() {  
        this._notificationService.getAlert().subscribe((alert: Notification) => {  
            this.notifications = [];  
            if (!alert) {  
                this.notifications = [];  
                return;  
            }  
            this.notifications.push(alert);  
            setTimeout(() => {  
                this.notifications = this.notifications.filter(x => x !== alert);  
            }, 2000);  
        });  
    }  
  
    removeNotification(notification: Notification) { 
        setTimeout(()=>{
        this.notifications = this.notifications.filter(x => x !== notification);  
    },10000) 
    }  
  
    /**Set css class for Alert -- Called from alert component**/     
    cssClass(notification: Notification) {  
        if (!notification) {  
            return;  
        }  
        switch (notification.type) {  
            case NotificationType.Success:  
                return 'toast-success';  
            case NotificationType.Error:  
                return 'toast-error';  
            case NotificationType.Info:  
                return 'toast-info';  
            case NotificationType.Warning:  
                return 'toast-warning';  
        }  
    }  
}  