import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WeightSettingComponent } from './weight-setting.component';

describe('WeightSettingComponent', () => {
  let component: WeightSettingComponent;
  let fixture: ComponentFixture<WeightSettingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WeightSettingComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WeightSettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
