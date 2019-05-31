import { TestBed, async, inject } from '@angular/core/testing';

import { PretendGuard } from './pretend.guard';

describe('PretendGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PretendGuard]
    });
  });

  it('should ...', inject([PretendGuard], (guard: PretendGuard) => {
    expect(guard).toBeTruthy();
  }));
});
