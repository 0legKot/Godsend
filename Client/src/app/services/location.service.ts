import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LocationService {
    geolocationPosition?: Position;

    constructor() { }

    getLocation() {
        if (window.navigator && window.navigator.geolocation) {
            window.navigator.geolocation.getCurrentPosition(
                position => {
                    this.geolocationPosition = position;
                    console.log(position);
                },
                error => {
                    switch (error.code) {
                        case 1:
                            console.log('Permission Denied');
                            break;
                        case 2:
                            console.log('Position Unavailable');
                            break;
                        case 3:
                            console.log('Timeout');
                            break;
                    }
                }
            );
        }
    }

}
