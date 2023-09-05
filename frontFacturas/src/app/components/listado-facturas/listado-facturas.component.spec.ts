import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListadoFacturasComponent } from './listado-facturas.component';

describe('ListadoFacturasComponent', () => {
  let component: ListadoFacturasComponent;
  let fixture: ComponentFixture<ListadoFacturasComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ListadoFacturasComponent]
    });
    fixture = TestBed.createComponent(ListadoFacturasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
