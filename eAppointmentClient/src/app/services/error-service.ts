import { HttpErrorResponse } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { FlexiToastService } from 'flexi-toast';

@Injectable({
  providedIn: 'root'
})
export class ErrorService {

  readonly #toast = inject(FlexiToastService);

  errorHandler(err: HttpErrorResponse) {

    console.log(err);

    let message = "An error occurred"; // Default message

    switch (err.status) {
      case 0:
        message = "API is not available";
        break;
      case 400:
        message = "Bad Request – Invalid request";
        break;
      case 401:
        message = "Unauthorized – Access is denied";
        break;
      case 403:
        message = "Forbidden – You do not have permission for this action";
        break;
      case 404:
        message = "API not found";
        break;
      case 409:
        message = "Conflict – Conflicting data";
        break;
      case 500:
        message = "Internal Server Error – Server error occurred";
        break;
      case 503:
        message = "Service Unavailable – The service is temporarily unavailable";
        break;
      default:
        message = err.error?.message || "An unexpected error occurred";
        break;
    }

    this.#toast.showToast(message, "error", "error");
  }
}
