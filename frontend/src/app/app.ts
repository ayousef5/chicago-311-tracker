import { Component, OnInit, computed, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';

interface ServiceRequest {
  srNumber: string;
  srType: string;
  status: string;
  origin: string;
  ownerDepartment: string;
  createdDate: string;
  streetAddress: string;
  city: string;
  zipCode: string;
}

@Component({
  selector: 'app-root',
  imports: [CommonModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {

  requests = signal<ServiceRequest[]>([]);
  loading = signal(true);
  error = signal('');

  // Filter state, updated by the search box and status dropdown
  searchText = signal('');
  statusFilter = signal('All');

  // Summary counts recalculate automatically whenever requests() changes.
  // The API returns "Open" or "Completed", so anything not Open counts as closed.
  totalCount = computed(() => this.requests().length);
  openCount = computed(() => this.requests().filter(r => this.isOpen(r)).length);
  closedCount = computed(() => this.totalCount() - this.openCount());

  // The table shows this list; it recalculates when the data or either filter changes
  filteredRequests = computed(() => {
    const search = this.searchText().trim().toLowerCase();
    const status = this.statusFilter();

    return this.requests().filter(r => {
      const matchesSearch =
        !search ||
        r.srNumber.toLowerCase().includes(search) ||
        (r.streetAddress ?? '').toLowerCase().includes(search) ||
        r.srType.toLowerCase().includes(search);

      const matchesStatus =
        status === 'All' ||
        (status === 'Open' && this.isOpen(r)) ||
        (status === 'Closed' && !this.isOpen(r));

      return matchesSearch && matchesStatus;
    });
  });

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.loadRequests();
  }

  loadRequests(): void {
    this.http
      .get<ServiceRequest[]>('http://localhost:5181/api/ServiceRequests')
      .subscribe({
        next: (data) => {
          this.requests.set(data);
          this.loading.set(false);
        },
        error: () => {
          this.error.set('Could not connect to the API.');
          this.loading.set(false);
        }
      });
  }

  onSearch(event: Event): void {
    this.searchText.set((event.target as HTMLInputElement).value);
  }

  onStatusChange(event: Event): void {
    this.statusFilter.set((event.target as HTMLSelectElement).value);
  }

  isOpen(request: ServiceRequest): boolean {
    return request.status === 'Open';
  }
}
