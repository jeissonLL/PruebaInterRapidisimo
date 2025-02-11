import { Component, OnInit } from '@angular/core';
import { DashboardService } from '../../services/dashboard.service';
import { event } from '../../interfaces/event';
import { NgxPaginationModule } from 'ngx-pagination';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgFor } from '@angular/common'; 

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [FormsModule, RouterModule, NgxPaginationModule, NgFor],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {
  events: event[] = [];
  filteredEvents: event[] = [];
  searchTerm: string = '';
  currentPage: number = 1;
  itemsPerPage: number = 10;

  constructor(private dashboardService: DashboardService) {}

  ngOnInit(): void {
    this.dashboardService.getEvents().subscribe((data) => {
      this.events = data;
      this.filteredEvents = data;
    });
  }

  search(): void {
    this.filteredEvents = this.events.filter((event) =>
      event.name.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }
}
