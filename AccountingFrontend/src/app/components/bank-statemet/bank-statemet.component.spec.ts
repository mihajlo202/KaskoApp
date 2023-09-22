import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BankStatemetComponent } from './bank-statemet.component';

describe('BankStatemetComponent', () => {
  let component: BankStatemetComponent;
  let fixture: ComponentFixture<BankStatemetComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BankStatemetComponent]
    });
    fixture = TestBed.createComponent(BankStatemetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
