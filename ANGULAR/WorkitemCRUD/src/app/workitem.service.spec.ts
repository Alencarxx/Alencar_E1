/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { WorkitemService } from './workitem.service';

describe('WorkitemService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [WorkitemService]
    });
  });

  it('should ...', inject([WorkitemService], (service: WorkitemService) => {
    expect(service).toBeTruthy();
  }));
});
